namespace EGAR.Infrastructure.RabbitMQ.Publishers;

public class RabbitMqPublisher : IPublisher
{
    private readonly IBus _bus;
    private readonly ILogger<OrderStatusPublisher> _logger;
    public RabbitMqPublisher(IBus _bus,
        ILogger<OrderStatusPublisher> _logger)
        : base()
    {
        _bus = bus;
        _logger = _logger;
    }

    public async Task<bool> PublishAsync(object message)
    {
        try
        {
            await _bus.Publish(message);
            _logger.LogInformation("Message {MessageType} is published: {Message}", message.GetType().Name, message);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Message {MessageType} was not published: {Message}", message.GetType().Name, message);
        }

        return false;
    }
}

