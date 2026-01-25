using EGAR.SharedKernel.Publishers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EGAR.Infrastructure.RabbitMQ.Publishers;

public class RabbitMqPublisher : IPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<RabbitMqPublisher> _logger;
    public RabbitMqPublisher(
        IPublishEndpoint publishEndpoint,
        ILogger<RabbitMqPublisher> logger)
        : base()
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<bool> PublishAsync(object @event, CancellationToken ct)
    {
        try
        {
            await _publishEndpoint.Publish(@event, ct);
            _logger.LogInformation("Message {MessageType} is published: {Message}", @event.GetType().Name, @event);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Message {MessageType} was not published: {Message}", @event.GetType().Name, @event);
        }

        return false;
    }
}
