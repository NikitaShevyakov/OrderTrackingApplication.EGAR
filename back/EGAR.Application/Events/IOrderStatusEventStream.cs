using EGAR.MessageBus.Contracts.Orders;
namespace EGAR.Application.Events;

public interface IOrderStatusEventStream
{
    IAsyncEnumerable<OrderStatusChangedEvent> Subscribe(CancellationToken ct);
}
