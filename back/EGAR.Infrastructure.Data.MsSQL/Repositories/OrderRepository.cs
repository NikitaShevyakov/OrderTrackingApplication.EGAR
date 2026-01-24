using EGAR.Application.Interfaces;
using EGAR.Domain.Models;
using EGAR.Infrastructure.Data.MsSQL.Context;
using EGAR.Infrastructure.Data.MsSQL.Entities;
using EGAR.Infrastructure.Data.MsSQL.Mapping;
using EGAR.Infrastructure.Data.MsSQL.Repositories.Base;
using EGAR.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;

namespace EGAR.Infrastructure.Data.MsSQL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly MsSqlRepository<OrderEntity> _baseRepo;

        public OrderRepository(AppDbContext context)
        {
            _baseRepo = new MsSqlRepository<OrderEntity>(context);
        }

        public async Task<PageResult<Order>> GetByPageAsync(int page, int limit, CancellationToken ct = default)
        {
            var data = (await _baseRepo.Query()
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(ct))
                .Select(x => x.ToDomain())
                .ToList();

            var total = await _baseRepo.Query().CountAsync();
            return new PageResult<Order>(data, total, page, limit);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken ct = default)
        {
            var entity = await _baseRepo.GetAllAsync(ct);
            return entity.Select(x => x.ToDomain());
        }

        public async Task<Order?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _baseRepo.GetByIdAsync(id, ct);
            return entity?.ToDomain();
        }

        public async Task<Result<Order>> AddAsync(Order domain, CancellationToken ct = default)
        {
            var orderEntity = domain.ToNewEntity();
            var result = await _baseRepo.AddAsync(orderEntity, ct);
            domain.Id = result.Value.Id;
            return result.IsSuccess
                ? Result<Order>.Success(domain)
                : Result<Order>.Failure(result.ErrorType, result.ErrorMessage);
        }                              

        public async Task<Result<Order>> UpdateAsync(Order domain, CancellationToken ct = default)
        {
            var result = await _baseRepo.UpdateAsync(domain.ToEntity(), ct);
            return result.IsSuccess
                ? Result<Order>.Success(result.Value.ToDomain())
                : Result<Order>.Failure(result.ErrorType, result.ErrorMessage);
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
        {
            var result = await _baseRepo.DeleteAsync(id, ct);
            return result.IsSuccess
                ? Result.Success()
                : Result.Failure(result.ErrorType, result.ErrorMessage);
        }
    }
}
