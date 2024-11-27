using Odonto.MessageBus;

namespace Odonto.API.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    Task SendMessage(BaseMessage baseMessage, string queueName);
}
