using EGAR.Domain.Models;

namespace EGAR.Application.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> CreateOrderAsync(Order order, CancellationToken ct = default);
    }
}