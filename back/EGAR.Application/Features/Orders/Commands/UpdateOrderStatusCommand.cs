using EGAR.Application.Interfaces;
using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Models;
using MediatR;

namespace EGAR.Application.Features.Orders.Commands;

public record UpdateOrderStatusCommand(int Id, OrderStatus Status) 
    : IRequest<Result<Order>>;

public class UpdateOrderStatusCommandHandler 
    : IRequestHandler<UpdateOrderStatusCommand, Result<Order>>
{
    readonly IOrderService _orderService;

    public UpdateOrderStatusCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<Result<Order>> Handle(UpdateOrderStatusCommand command, CancellationToken ct)
        => await _orderService.ChangeStatusAsync(command.Id, command.Status, ct);        
}
