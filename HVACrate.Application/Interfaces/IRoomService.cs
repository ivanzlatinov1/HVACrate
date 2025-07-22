using HVACrate.Application.Models;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Interfaces;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    public interface IRoomService : ICalculator
    {
        Task<Result<RoomModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? buildingId, CancellationToken cancellationToken = default);
        Task<RoomModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<RoomModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(RoomModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(RoomModel model, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> GetBuildingIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<string> GetRoomNumberAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsRoomEnclosed(Guid id, CancellationToken cancellationToken = default);
    }
}
