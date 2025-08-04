using HVACrate.Application.Models;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Interfaces;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    /// <summary>
    /// Provides services for managing room data including CRUD operations, metadata queries, and calculations.
    /// </summary>
    public interface IRoomService : ICalculator
    {
        /// <summary>
        /// Retrieves all rooms as read-only models, optionally filtered by building ID and query parameters.
        /// </summary>
        /// <param name="query">Pagination and filtering options.</param>
        /// <param name="buildingId">Optional ID of the associated building.</param>
        Task<Result<RoomModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? buildingId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves a room by its ID as a read-only model.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        Task<RoomModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a room by its ID with full access for editing or other operations.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        Task<RoomModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new room in the database.
        /// </summary>
        /// <param name="model">The room model to be created.</param>
        Task CreateAsync(RoomModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing room with new information.
        /// </summary>
        /// <param name="model">The updated room model.</param>
        Task UpdateAsync(RoomModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a room by marking it as inactive without removing it from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the room to delete.</param>
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the building ID associated with the specified room.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        Task<Guid> GetBuildingIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the room number of the specified room.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        Task<string> GetRoomNumberAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all rooms that are marked as enclosed, using pagination.
        /// </summary>
        /// <param name="pagination">Pagination parameters such as page and limit.</param>
        Task<RoomModel[]> GetAllEnclosedRoomsAsync(Pagination pagination, CancellationToken cancellationToken = default);
    }
}
