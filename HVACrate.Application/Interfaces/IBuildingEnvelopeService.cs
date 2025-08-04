using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Enums;

namespace HVACrate.Application.Interfaces
{
    /// <summary>
    /// Provides building-envelope-related services including creation, retrieval, update, and soft-deletion, 
    /// as well as multiple calculations for heating and cooling loads.
    /// </summary>
    public interface IBuildingEnvelopeService
    {
        /// <summary>
        /// Gets all building envelopes for the specified room as read-only models.
        /// </summary>
        /// <param name="roomId">The ID of the room to filter by.</param>
        Task<List<BuildingEnvelopeModel>> GetAllAsReadOnlyAsync(Guid? roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all outer walls for a given room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<OuterWallModel[]> GetOuterWallsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all openings (e.g., windows, doors) for a given room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<OpeningModel[]> GetOpeningsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all openings for a room filtered by direction.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <param name="direction">The direction to filter openings by (e.g., North, South).</param>
        Task<OpeningModel[]> GetOpeningsByRoomAndDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves internal fences (indoor walls or other interior) for a specific room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<InternalFenceModel[]> GetInternalFencesByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all floor models associated with a specific room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<FloorModel[]> GetFloorsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all roof models associated with a specific room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<RoofModel[]> GetRoofsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a building envelope by its ID as a read-only model.
        /// </summary>
        /// <param name="id">The ID of the building envelope.</param>
        Task<BuildingEnvelopeModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a building envelope by its ID for editing or other operations.
        /// </summary>
        /// <param name="id">The ID of the building envelope.</param>
        Task<BuildingEnvelopeModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new building envelope model in the database.
        /// </summary>
        /// <param name="model">The building envelope model to create.</param>
        Task CreateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing building envelope model.
        /// </summary>
        /// <param name="model">The updated building envelope model.</param>
        Task UpdateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a building envelope by marking it as inactive.
        /// </summary>
        /// <param name="id">The ID of the building envelope to delete.</param>
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the outer wall in a given direction for a room, if it exists.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <param name="direction">The direction to find the wall (e.g., North, East).</param>
        Task<OuterWallModel?> GetWallByDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a wall exists in the specified direction for a room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <param name="direction">The direction to check.</param>
        Task<bool> IsThereAWallOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if an opening exists in the specified direction for a room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <param name="direction">The direction to check.</param>
        Task<bool> IsThereAnOpeningOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines if a roof exists in the specified room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<bool> IsThereARoofInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines if a floor exists in the specified room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<bool> IsThereAFloorInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the number of internal fences in a given room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<long> GetInternalFencesCountByRoom(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the number of openings in a given room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        Task<long> GetOpeningsCountByRoom(Guid roomId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates the total area to subtract (e.g., openings) from a building envelope (wall or interior wall).
        /// </summary>
        /// <param name="buildingEnvelopeId">The ID of the building envelope.</param>
        Task<double> GetAreaToBeSubtracted(Guid buildingEnvelopeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates the temperature coefficient based on material density and type.
        /// </summary>
        /// <param name="density">The material density (kg/m³).</param>
        /// <param name="type">The material type (e.g., wood, brick).</param>
        double CalculateTemperatureCoefficient(double density, string type);

        /// <summary>
        /// Calculates heat infiltration through the building envelope.
        /// </summary>
        /// <param name="buildingEnvelope">The building envelope model to analyze.</param>
        double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope);

        /// <summary>
        /// Calculates heat transmission through the building envelope.
        /// </summary>
        /// <param name="buildingEnvelope">The building envelope model to analyze.</param>
        Task<double> CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope);
    }
}
