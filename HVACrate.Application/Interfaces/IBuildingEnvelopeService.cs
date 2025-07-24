using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Enums;

namespace HVACrate.Application.Interfaces
{
    public interface IBuildingEnvelopeService
    {
        Task<List<BuildingEnvelopeModel>> GetAllAsReadOnlyAsync(Guid? roomId, CancellationToken cancellationToken = default);
        Task<OuterWallModel[]> GetOuterWallsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<OpeningModel[]> GetOpeningsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<InternalFenceModel[]> GetInternalFencesByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<FloorModel[]> GetFloorsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<RoofModel[]> GetRoofsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<BuildingEnvelopeModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BuildingEnvelopeModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<OuterWallModel?> GetWallByDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereAWallOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereAnOpeningOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default);
        Task<bool> IsThereARoofInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<bool> IsThereAFloorInRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<long> GetInternalFencesCountByRoom(Guid roomId, CancellationToken cancellationToken = default);
        Task<long> GetOpeningsCountByRoom(Guid roomId, CancellationToken cancellationToken = default);
        double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope);
        double CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope);
    }
}
