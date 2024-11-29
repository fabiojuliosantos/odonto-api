using Odonto.MessageBus;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Odonto.Application.Interfaces;
using Odonto.API.DTO.Dentistas;

namespace Odonto.Application.Services;

public class RabbitMqMessageSender : IRabbitMqMessageSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _username;
    private IConnection _connection;

    public RabbitMqMessageSender()
    {
        _hostName = "localhost";
        _password = "guest";
        _username = "guest";
    }

    public async Task SendMessage(BaseMessage message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            UserName = _username,
            Password = _password,
        };

        _connection = await factory.CreateConnectionAsync();

        var channel = await _connection.CreateChannelAsync();

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
        var json = JsonSerializer.Serialize<DentistasCadastroDTO>((DentistasCadastroDTO)message, options);
        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }
}
