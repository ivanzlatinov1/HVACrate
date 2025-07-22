using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Projects;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Repositories.Projects;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Services
{
    public class ProjectService(IProjectRepository projectRepository) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Result<ProjectModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? creatorId, CancellationToken cancellationToken = default)
        {
            Result<Project> projects = await this._projectRepository
                .GetAllAsReadOnlyAsync(query.ToQuery(), creatorId, cancellationToken)
                .ConfigureAwait(false);

            return new Result<ProjectModel>(projects.Count, [.. projects.Items.Select(x => x.ToModel(false))]);
        }

        public async Task<ProjectModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Project project = await this._projectRepository
                .GetByIdAsReadOnlyAsync(id, cancellationToken)
                .ConfigureAwait(false)
                ?? throw new Exception("User not found");

            return project.ToModel();
        }

        public async Task<ProjectModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Project project = await this._projectRepository
                .GetByIdAsync(id, cancellationToken)
                .ConfigureAwait(false)
                ?? throw new Exception("User not found");

            return project.ToModel();
        }

        public async Task<DateTimeOffset> GetLastlyModifiedDateAsync(Guid id, CancellationToken cancellationToken = default)
            => await this._projectRepository
            .GetLastTimeModifiedDateAsync(id, cancellationToken)
            .ConfigureAwait(false);

        public async Task CreateAsync(ProjectModel model, CancellationToken cancellationToken = default)
        {
            await this._projectRepository.CreateAsync(model.ToEntity(false), cancellationToken).ConfigureAwait(false);
            await this._projectRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ProjectModel model, CancellationToken cancellationToken = default)
        {
            this._projectRepository.Update(model.ToEntity(false));
            await this._projectRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Project project = await this._projectRepository
                .GetByIdAsync(id, cancellationToken)
                .ConfigureAwait(false)
                ?? throw new Exception("Project not found");

            this._projectRepository.SoftDelete(project);
            await this._projectRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
