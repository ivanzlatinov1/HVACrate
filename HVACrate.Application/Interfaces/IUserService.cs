using HVACrate.Application.Models;
using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    /// <summary>
    /// Provides services for managing HVAC system users and their roles.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users with optional pagination and filtering.
        /// </summary>
        /// <param name="query">Query model for pagination and filtering options.</param>
        Task<Result<HVACUserModel>> GetAllAsync(BaseQueryModel query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        Task<HVACUserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a user from the database by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to remove.</param>
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the roles for multiple users.
        /// </summary>
        /// <param name="ids">An array of user IDs to retrieve roles for.</param>
        Task<Dictionary<Guid, string>> GetRolesAsync(Guid[] ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the role of a specific user by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        Task<string> GetRoleAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
