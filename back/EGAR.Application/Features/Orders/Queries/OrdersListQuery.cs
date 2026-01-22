using EGAR.Domain.Models;
using EGAR.SharedKernel;
using EGAR.Application.Interfaces;
using MediatR;

namespace EGAR.Application.Features.Orders.Queries;

public record OrdersListQuery() : IRequest<Result<IEnumerable<Order>>>;

public class OrdersListQueryHandler : IRequestHandler<OrdersListQuery, Result<IEnumerable<Order>>>
{
    private readonly IRepository<Order> _repository;

    public OrdersListQueryHandler(IRepository<Order> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Order>>> Handle(OrdersListQuery request, CancellationToken ct = default)
    {
        var orders = await _repository.GetAllAsync(ct);
        return Result<IEnumerable<Order>>.Success(orders);
    }
}