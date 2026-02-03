using HVACrate.Application.Models;
using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Interfaces;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    /// <summary>
    /// Provides building-related services including creation, retrieval, update, and soft-deletion, 
    /// as well as calculations and floor management.
    /// </summary>
    public interface IBuildingService : ICalculator
    {
        /// <summary>
        /// Retrieves all buildings in a read-only form, filtered by project ID and query options.
        /// </summary>
        /// <param name="query">Pagination and filtering options.</param>
        /// <param name="projectId">Optional ID of the associated project.</param>
        Task<Result<BuildingModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a single building by its ID as a read-only model.
        /// </summary>
        /// <param name="id">The unique identifier of the building.</param>
        Task<BuildingModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a single building by its ID for editing or other operations.
        /// </summary>
        /// <param name="id">The unique identifier of the building.</param>
        Task<BuildingModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new building model in the database.
        /// </summary>
        /// <param name="model">The building model to create.</param>
        Task CreateAsync(BuildingModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing building model with new information.
        /// </summary>
        /// <param name="model">The updated building model.</param>
        Task UpdateAsync(BuildingModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the entity from the database.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a building by marking it as inactive without removing it from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the building to delete.</param>
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the total number of floors for a building.
        /// </summary>
        /// <param name="id">The unique identifier of the building.</param>
        Task<int> GetTotalFloors(Guid id, CancellationToken cancellationToken = default);
    }
}
