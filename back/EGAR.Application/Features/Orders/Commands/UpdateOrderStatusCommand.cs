using EGAR.Application.Interfaces.Repositories;
using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.MessageBus.Contracts.Orders;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using MediatR;

namespace EGAR.Application.Features.Orders.Commands;

public record UpdateOrderStatusCommand(int Id, OrderStatus Status) 
    : IRequest<Result<Order>>;

public class UpdateOrderStatusCommandHandler 
    : IRequestHandler<UpdateOrderStatusCommand, Result<Order>>
{
    readonly IOrderRepository _orderRepository;
    readonly SharedKernel.Publishers.IPublisher _publisher;

    public UpdateOrderStatusCommandHandler(
        IOrderRepository orderRepository,
        SharedKernel.Publishers.IPublisher publisher
        )
    {
        _orderRepository = orderRepository;
        _publisher = publisher;
    }

    public async Task<Result<Order>> Handle(
        UpdateOrderStatusCommand command,
        CancellationToken ct)
    {
        var order = await _orderRepository.GetByIdAsync(command.Id, ct);

        if (order == null)
            return Result<Order>.Failure(ErrorType.NotFound, "Order not found");

        var oldStatus = order.Status;
        var newStatus = command.Status;

        order.Status = newStatus;
        order.UpdatedAt = DateTimeOffset.UtcNow;
        var result = await _orderRepository.UpdateAsync(order, ct);

        if (!result.IsSuccess) return result;

        var @event = new OrderStatusChangedEvent(order.Id, oldStatus, newStatus, DateTime.UtcNow);
        await _publisher.PublishAsync(@event, ct);

        return result;
    }
}