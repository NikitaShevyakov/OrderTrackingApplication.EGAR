using EGAR.Domain.Enums;

namespace EGAR.Infrastructure.RabbitMQ.MessageBus.Events;

public record OrderStatusChangedEvent(
    int OrderId,
    OrderStatus OldStatus,
    OrderStatus NewStatus,
    DateTimeOffset ChangedAt);
