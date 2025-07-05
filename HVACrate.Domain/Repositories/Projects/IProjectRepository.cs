using HVACrate.Domain.Entities;

namespace HVACrate.Domain.Repositories.Projects
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<DateTimeOffset> GetLastTimeModifiedDateAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
