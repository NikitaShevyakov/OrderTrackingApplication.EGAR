using EGAR.MessageBus.Contracts.Orders;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace EGAR.Application.Events;

public class InMemoryOrderStatusEventStream    
    : IOrderStatusEventStream, IOrderStatusEventDispatcher
{
    private readonly Channel<OrderStatusChangedEvent> _channel = 
        Channel.CreateUnbounded<OrderStatusChangedEvent>();

    public async Task PublishAsync(
        OrderStatusChangedEvent evt,
        CancellationToken ct = default)
    {
        await _channel.Writer.WriteAsync(evt, ct);
    }

    public async IAsyncEnumerable<OrderStatusChangedEvent> Subscribe(
        [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var evt in _channel.Reader.ReadAllAsync(ct))
        {
            yield return evt;
        }
    }
}
