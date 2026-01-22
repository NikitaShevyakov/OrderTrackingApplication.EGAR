using EGAR.Application.Interfaces;
using EGAR.Domain.Models;

namespace EGAR.Infrastructure.Services;

public class OrderService : IOrderService
{
    readonly IOrderRepository _orderRepository;
    //private readonly IEventBus _bus;

    public OrderService(
        IOrderRepository orderRepository
        //IEventBus bus
        )
    {
        _orderRepository = orderRepository;
        //_bus = bus;
    }

    public async Task<Order> CreateOrderAsync(Order order, CancellationToken ct = default)
    {

        var newOrder = await _orderRepository.CreateOrderAsync(order, ct);
        //await _bus.PublishAsync("order.status.changed", new
        //{
        //    order.Id,
        //    order.OrderNumber,
        //    order.Status,
        //    order.UpdatedAt
        //}, ct);
        return newOrder;
    }

    //public async Task<Order> ChangeStatusAsync(Guid id, OrderStatus status, CancellationToken ct)
    //{
    //    var order = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Order not found");
    //    order.Status = status;
    //    order.UpdatedAt = DateTimeOffset.UtcNow;
    //    await _repo.UpdateAsync(order, ct);
    //    await _bus.PublishAsync("order.status.changed", new
    //    {
    //        order.Id,
    //        order.OrderNumber,
    //        order.Status,
    //        order.UpdatedAt
    //    }, ct);
    //    return order;
    //}
}
