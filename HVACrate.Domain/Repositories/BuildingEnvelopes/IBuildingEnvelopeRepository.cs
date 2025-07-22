using HVACrate.Domain.Entities;
using HVACrate.Domain.Entities.BuildingEnvelopes;

namespace HVACrate.Domain.Repositories.BuildingEnvelopes
{
    public interface IBuildingEnvelopeRepository : IBaseRepository<BuildingEnvelope>
    {
        Task<OuterWall?> GetWallByDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereAWallOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereAnOpeningOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereARoofInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<bool> IsThereAFloorInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
    }
}
