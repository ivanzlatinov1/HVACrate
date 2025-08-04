using HVACrate.Application.Models;
using HVACrate.Application.Models.Projects;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    /// <summary>
    /// Provides services for managing project model data, including CRUD operations and project metadata.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Retrieves all projects as read-only models, filtered by creator ID and query parameters.
        /// </summary>
        /// <param name="query">Pagination and filtering options.</param>
        /// <param name="creatorId">ID of the project creator (user).</param>
        Task<Result<ProjectModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? creatorId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves a project by its ID as a read-only model.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        Task<ProjectModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a project by its ID with full access for editing or other operations.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        Task<ProjectModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new project in the database.
        /// </summary>
        /// <param name="model">The project model to be created.</param>
        Task CreateAsync(ProjectModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing project with new information.
        /// </summary>
        /// <param name="model">The updated project model.</param>
        Task UpdateAsync(ProjectModel model, CancellationToken cancelToken = default);

        /// <summary>
        /// Retrieves the name of a project by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        Task<string?> GetProjectNameAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the last modified date of a project.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        Task<DateTimeOffset> GetLastlyModifiedDateAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a project by marking it as inactive without removing it from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the project to delete.</param>
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
