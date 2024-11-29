using Odonto.MessageBus;

namespace Odonto.Application.Interfaces;

public interface IRabbitMqMessageSender
{
    Task SendMessage(BaseMessage baseMessage, string queueName);
}
