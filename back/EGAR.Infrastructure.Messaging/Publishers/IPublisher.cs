namespace EGAR.Infrastructure.RabbitMQ.Publishers;

public interface IPublisher
{
    Task<bool> PublishAsync(object @event);
}