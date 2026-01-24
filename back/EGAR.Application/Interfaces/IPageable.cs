using EGAR.SharedKernel.Models;

namespace EGAR.Application.Interfaces;

public interface IPageable<T>
{
    Task<PageResult<T>> GetByPageAsync(int page, int limit, CancellationToken cancellationToken = default);
}
