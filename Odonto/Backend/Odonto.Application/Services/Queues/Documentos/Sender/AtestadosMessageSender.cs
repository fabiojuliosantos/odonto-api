using Odonto.Application.DTO;
using Odonto.MessageBus;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Odonto.Application.Interfaces;
using Odonto.API.DTOs.Documentos;

namespace Odonto.Application.Services.Queues.Documentos.Sender;

public class AtestadosMessageSender : IAtestadosMessageSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _username;
    private IConnection _connection;

    public AtestadosMessageSender()
    {
        _hostName = "localhost";
        _username = "fabio.julio";
        _password = "@dm1n";
    }

    public async Task SendMessage(BaseMessage message, string queueName)
    {
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = _hostName,
            Password = _password,
            UserName = _username,
        };

        _connection = await factory.CreateConnectionAsync();

        IChannel channel = await _connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, false, false, false, arguments: null);

        byte[] body = GetMessageAsByteArray(message);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
    }

    private byte[] GetMessageAsByteArray(BaseMessage message)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var json = JsonSerializer.Serialize((Atestado)message, options);
        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }
}
