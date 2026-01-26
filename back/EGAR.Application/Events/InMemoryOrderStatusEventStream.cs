using EGAR.MessageBus.Contracts.Orders;
using System.Threading.Channels;

namespace EGAR.Application.Events;

public class InMemoryOrderStatusEventStream    
    : IOrderStatusEventStream, IOrderStatusEventDispatcher
{
    readonly List<Channel<OrderStatusChangedEvent>> _channels = new();
    readonly object _lock = new();

    public async Task PublishAsync(OrderStatusChangedEvent evt, CancellationToken ct = default)
    {
        List<Channel<OrderStatusChangedEvent>> channels;

        lock (_lock)
            channels = _channels.ToList();

        foreach (var c in channels)
        {
            await c.Writer.WriteAsync(evt, ct);
        }
    }
    public IAsyncEnumerable<OrderStatusChangedEvent> Subscribe(CancellationToken ct)
    {
        var channel = Channel.CreateUnbounded<OrderStatusChangedEvent>();

        lock (_lock)
            _channels.Add(channel);

        return channel.Reader.ReadAllAsync(ct);
    }
}
