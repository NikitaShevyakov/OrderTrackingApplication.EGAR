using EGAR.Application.Interfaces.Repositories;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using MediatR;

namespace EGAR.Application.Features.Orders.Commands
{
    public record DeleteOrderCommand(int Id) : IRequest<Result>;

    public class DeleteOrderCommandHandler 
        : IRequestHandler<DeleteOrderCommand, Result>
    {
        readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(DeleteOrderCommand command, CancellationToken ct)
        {
            if (command.Id <= 0)
                return Result.Failure(ErrorType.Validation, "Order Id isn't correct");

            var order = await _orderRepository.GetByIdAsync(command.Id, ct);

            if (order == null)
                return Result.Failure(ErrorType.Validation, "Order isn't exist");

            return await _orderRepository.DeleteAsync(order.Id, ct);
        }
    }
}
