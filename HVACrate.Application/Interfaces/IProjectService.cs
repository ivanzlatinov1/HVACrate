using HVACrate.Application.Models.Projects;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    public interface IProjectService
    {
        Task<Result<ProjectModel>> GetAllAsReadOnlyAsync(ProjectQueryModel query, Guid? creatorId, CancellationToken cancellationToken = default);
        Task<ProjectModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProjectModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(ProjectModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(ProjectModel model, CancellationToken cancelToken = default);
        Task<DateTimeOffset> GetLastlyModifiedDateAsync(Guid id, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
