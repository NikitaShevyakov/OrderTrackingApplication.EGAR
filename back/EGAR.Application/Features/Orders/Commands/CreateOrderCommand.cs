using EGAR.Application.Interfaces;
using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using MediatR;

namespace EGAR.Application.Features.Orders.Commands
{
    public record CreateOrderCommand(string OrderNumber, string Description)
        : IRequest<Result<Order>>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Order>>
    {
        readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<Order>> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(request.OrderNumber))
                return Result<Order>.Failure(ErrorType.Validation, "Order number is required");

            if (string.IsNullOrWhiteSpace(request.Description))
                return Result<Order>.Failure(ErrorType.Validation, "Description is required");

            var order = new Order
            {
                OrderNumber = request.OrderNumber,
                Description = request.Description,
                Status = OrderStatus.Created,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            return await _orderRepository.AddAsync(order, ct); ;
        }
    }
}
