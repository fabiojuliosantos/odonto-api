using Odonto.API.DTOs.Documentos;
using Odonto.Application.Interfaces;
using Odonto.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Odonto.Application.Services.Queues.Documentos.Sender;

public class ReceitasMessageSender : IReceitasMessageSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _username;
    private IConnection _connection;

    public ReceitasMessageSender()
    {
        _hostName = "localhost";
        _password = "guest";
        _username = "guest";
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
        var json = JsonSerializer.Serialize((ReceitaDTO)message, options);
        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }
}
