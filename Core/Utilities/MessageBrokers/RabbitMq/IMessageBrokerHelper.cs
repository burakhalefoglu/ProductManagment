using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Core.Utilities.MessageBrokers.RabbitMq;

public interface IMessageBrokerHelper
{
    Task<IResult> QueueMessageAsync<T>(T messageModel, string q);
}