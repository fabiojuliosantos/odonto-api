using Odonto.MessageBus;

namespace Odonto.Application.Interfaces;

public interface IAtestadosMessageSender
{
    Task SendMessage(BaseMessage baseMessage, string queueName);

}
