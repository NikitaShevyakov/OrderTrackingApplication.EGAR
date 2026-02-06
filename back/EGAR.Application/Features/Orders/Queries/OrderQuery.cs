using EGAR.Application.Interfaces.Repositories;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using MediatR;

namespace EGAR.Application.Features.Orders.Queries;

public record OrderQuery(int Id) : IRequest<Result<Order>> { }

public class OrderQueryHandler : IRequestHandler<OrderQuery, Result<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<Order>> Handle(OrderQuery q, CancellationToken ct = default)
    {
        throw new Exception();
        throw new Exception();

        if (q.Id <=0)
            return Result<Order>.Failure(ErrorType.Validation, $"Parameter 'Id' must be a positive integer greater than 0. Received: {q.Id}");

        var order = await _orderRepository.GetByIdAsync(q.Id, ct);

        return order == null
            ? Result<Order>.Failure(ErrorType.NotFound, $"Order with Id:{q.Id} wasn't found")
            : Result<Order>.Success(order);
    }
}