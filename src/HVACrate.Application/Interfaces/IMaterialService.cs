using HVACrate.Application.Models;
using HVACrate.Application.Models.Materials;

namespace HVACrate.Application.Interfaces
{
    /// <summary>
    /// Provides operations to manage material data including retrieval, creation, updating, and soft deletion.
    /// </summary>
    public interface IMaterialService
    {
        /// <summary>
        /// Retrieves all materials in a read-only format, filtered by query parameters.
        /// </summary>
        /// <param name="query">Pagination and filtering options.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task<List<MaterialModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a material by its ID as a read-only model.
        /// </summary>
        /// <param name="id">The unique identifier of the material.</param>
        Task<MaterialModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a material by its ID with full access for editing or other operations.
        /// </summary>
        /// <param name="id">The unique identifier of the material.</param>
        Task<MaterialModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new material model in the database.
        /// </summary>
        /// <param name="model">The material model to be created.</param>
        Task CreateAsync(MaterialModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing material with new information.
        /// </summary>
        /// <param name="model">The updated material model.</param>
        Task UpdateAsync(MaterialModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a material by marking it as inactive without removing it from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the material to delete.</param>
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the entity from the database.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
