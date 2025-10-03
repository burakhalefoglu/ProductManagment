namespace Core.Utilities.MessageBrokers.RabbitMq;

public interface IMessageConsumer
{
    void GetQueue(string q);
}