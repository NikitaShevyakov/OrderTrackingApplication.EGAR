using EGAR.Application.Interfaces;
using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.Infrastructure.RabbitMQ.MessageBus.Events;
using EGAR.Infrastructure.RabbitMQ.Publishers;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
namespace EGAR.Infrastructure.Services;

public class OrderService : IOrderService
{
    readonly IOrderRepository _orderRepository;
    readonly IPublisher _publisher;

    public OrderService(IOrderRepository orderRepository, IPublisher publisher)
    {
        _orderRepository = orderRepository;
        _publisher = publisher;
    }

    public async Task<Result<Order>> ChangeStatusAsync(int id, OrderStatus status, CancellationToken ct)
    {
        var order = await _orderRepository.GetByIdAsync(id, ct);

        if(order == null) 
            return Result<Order>.Failure(ErrorType.NotFound, "Order not found");
        
        var oldStatus = order.Status;
        var newStatus = status;

        order.Status = status;
        order.UpdatedAt = DateTimeOffset.UtcNow;
        var result = await _orderRepository.UpdateAsync(order, ct);

        if (!result.IsSuccess) return result;

        var message = new OrderStatusChangedEvent(order.Id, oldStatus, newStatus, DateTime.UtcNow);
        await _publisher.PublishAsync(message);

        return result;
    }
}
