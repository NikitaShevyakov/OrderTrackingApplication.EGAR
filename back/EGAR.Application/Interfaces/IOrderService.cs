using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Models;

namespace EGAR.Application.Interfaces;

public interface IOrderService
{
    Task<Result<Order>> ChangeStatusAsync(int id, OrderStatus status, CancellationToken ct = default);
}
