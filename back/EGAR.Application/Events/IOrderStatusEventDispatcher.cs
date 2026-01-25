using EGAR.MessageBus.Contracts.Orders;

namespace EGAR.Application.Events;

public interface IOrderStatusEventDispatcher
{
    Task PublishAsync(OrderStatusChangedEvent evt, CancellationToken ct = default);
}
