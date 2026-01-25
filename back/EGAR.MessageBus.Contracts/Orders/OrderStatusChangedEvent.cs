using EGAR.Domain.Enums;

namespace EGAR.MessageBus.Contracts.Orders;

public record OrderStatusChangedEvent(
    int OrderId,
    OrderStatus OldStatus,
    OrderStatus NewStatus,
    DateTimeOffset ChangedAt);
