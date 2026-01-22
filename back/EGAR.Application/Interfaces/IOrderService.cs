using EGAR.Domain.Models;

namespace EGAR.Application.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order, CancellationToken ct = default);
}
