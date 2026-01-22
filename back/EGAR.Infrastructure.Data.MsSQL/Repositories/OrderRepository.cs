using EGAR.Application.Interfaces;
using EGAR.Domain.Models;
using EGAR.Infrastructure.Data.MsSQL.Context;
using EGAR.Infrastructure.Data.MsSQL.Entities;
using EGAR.Infrastructure.Data.MsSQL.Mapping;
using EGAR.Infrastructure.Data.MsSQL.Repositories.Base;

namespace EGAR.Infrastructure.Data.MsSQL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly MsSqlRepository<OrderEntity> _baseRepo;

        public OrderRepository(AppDbContext context)
        {
            _baseRepo = new MsSqlRepository<OrderEntity>(context);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken ct = default)
        {
            var entity = await _baseRepo.GetAllAsync(ct);
            return entity.Select(x => x.ToDomain());
        }

        public async Task<Order?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _baseRepo.GetByIdAsync(id, ct);
            return entity.ToDomain();
        }

        public async Task AddAsync(Order domain, CancellationToken ct = default) =>        
            await _baseRepo.AddAsync(domain.ToEntity(), ct);                  

        public Task Update(Order domain)
        {
            _baseRepo.Update(domain.ToEntity());
            return Task.CompletedTask;
        }

        public Task Delete(Order domain)
        {
            _baseRepo.Delete(domain.ToEntity());
            return Task.CompletedTask;
        }

        public async Task<Order> CreateOrderAsync(Order order, CancellationToken ct = default)
        {
            var orderEntity = order.ToNewEntity();
            await _baseRepo.AddAsync(orderEntity, ct);
            await _baseRepo.SaveChangesAsync(ct);
            order.Id = orderEntity.Id;
            return order;
        }
    }
}
