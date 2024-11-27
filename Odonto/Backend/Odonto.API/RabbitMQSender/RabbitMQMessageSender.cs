using Odonto.MessageBus;
using RabbitMQ.Client;

namespace Odonto.API.RabbitMQSender;

public class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _username;
    private IConnection _connection;

    public RabbitMQMessageSender()
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
        IChannel channel = await _connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, false, false, false, arguments: null);

        byte[] body = GetMessageAsByteArray(message);

        // Remova a especificação do tipo genérico <IBasicProperties> e use null para basicProperties
        await channel.BasicPublishAsync(
            exchange: "",  // Aqui é um string vazio se você estiver usando uma fila direta
            routingKey: queueName,
            mandatory: false,
            basicProperties: null,
            body: body);
    }


    private byte[] GetMessageAsByteArray(BaseMessage message)
    {
        throw new NotImplementedException();
    }
}
