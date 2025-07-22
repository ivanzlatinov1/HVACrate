using HVACrate.Domain.Entities;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Domain.Repositories.Projects
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<Result<Project>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default);
        Task<DateTimeOffset> GetLastTimeModifiedDateAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
