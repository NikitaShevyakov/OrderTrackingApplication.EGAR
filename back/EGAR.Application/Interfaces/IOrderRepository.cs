using EGAR.Domain.Models;

namespace EGAR.Application.Interfaces;

public interface IOrderRepository 
    : IRepository<Order>, IPageable<Order> 
{
}
