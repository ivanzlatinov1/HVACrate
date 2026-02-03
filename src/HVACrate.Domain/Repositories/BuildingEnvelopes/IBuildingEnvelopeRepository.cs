using HVACrate.Domain.Entities;
using HVACrate.Domain.Entities.BuildingEnvelopes;

namespace HVACrate.Domain.Repositories.BuildingEnvelopes
{
    public interface IBuildingEnvelopeRepository : IBaseRepository<BuildingEnvelope>
    {
        Task<BuildingEnvelope[]> GetAllAsReadOnlyAsync(Guid? filterId = null, CancellationToken cancellationToken = default);
        Task<OuterWall[]> GetOuterWallsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<Opening[]> GetOpeningsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<Opening[]> GetOpeningsByRoomAndDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<InternalFence[]> GetInternalFencesByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<Floor[]> GetFloorsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<Roof[]> GetRoofsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<OuterWall?> GetWallByDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereAWallOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereAnOpeningOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereARoofInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<bool> IsThereAFloorInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<long> GetOpeningsCountByRoom(Guid roomId, CancellationToken cancellationToken = default);
        Task<long> GetInternalFencesCountByRoom(Guid roomId, CancellationToken cancellationToken = default);
        Task<double> GetAreaToBeSubtracted(Guid buildingEnvelopeId, CancellationToken cancellationToken = default);
    }
}
