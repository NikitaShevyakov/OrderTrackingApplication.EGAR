using EGAR.Application.Events;
using EGAR.MessageBus.Contracts.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EGAR.Infrastructure.RabbitMQ.Consumers;

public class OrderStatusUpdatedConsumer 
    : IConsumer<OrderStatusChangedEvent>
{
    private readonly ILogger<OrderStatusUpdatedConsumer> _logger;
    private readonly IOrderStatusEventDispatcher _dispatcher;

    public OrderStatusUpdatedConsumer(ILogger<OrderStatusUpdatedConsumer> logger,
        IOrderStatusEventDispatcher dispatcher)
    {
        _logger = logger;
        _dispatcher = dispatcher;
    }

    public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        var msg = context.Message;

        _logger.LogInformation($"Received message: order status was updated. " +
            $"OrderId:{msg.OrderId}, " +
            $"oldStatus:{msg.OldStatus}, " +
            $"newStatus:{msg.NewStatus} ");

        await _dispatcher.PublishAsync(
            new OrderStatusChangedEvent(
                msg.OrderId,
                msg.OldStatus,
                msg.NewStatus,
                msg.ChangedAt));
    }
}