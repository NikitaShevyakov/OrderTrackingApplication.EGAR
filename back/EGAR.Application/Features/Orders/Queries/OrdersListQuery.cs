using EGAR.Domain.Models;
using EGAR.Application.Interfaces;
using MediatR;
using EGAR.SharedKernel.Models;

namespace EGAR.Application.Features.Orders.Queries;

public record OrdersListQuery() 
    : IRequest<Result<PageResult<Order>>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}   

public class OrdersListQueryHandler 
    : IRequestHandler<OrdersListQuery, Result<PageResult<Order>>>
{
    private readonly IOrderRepository _orderRepository;

    public OrdersListQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<PageResult<Order>>> Handle(
        OrdersListQuery query,
        CancellationToken ct = default)
    {
        var orders = await _orderRepository.GetByPageAsync(query.Page, query.Limit, ct);
        return Result<PageResult<Order>>.Success(orders);
    }
}