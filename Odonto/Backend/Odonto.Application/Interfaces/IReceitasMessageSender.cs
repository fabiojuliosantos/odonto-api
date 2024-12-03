using Odonto.MessageBus;

namespace Odonto.Application.Interfaces;

public interface IReceitasMessageSender
{
    Task SendMessage(BaseMessage baseMessage, string queueName);

}
