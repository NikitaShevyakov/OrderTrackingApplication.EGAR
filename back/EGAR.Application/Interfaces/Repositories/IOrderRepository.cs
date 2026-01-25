using EGAR.Domain.Models;

namespace EGAR.Application.Interfaces.Repositories;

public interface IOrderRepository 
    : IRepository<Order>, IPageable<Order> 
{
}
