using HVACrate.Domain.Entities.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Domain.Repositories.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.BuildingEnvelopes
{
    public class BuildingEnvelopeRepository(HVACrateDbContext context)
        : BaseRepository<BuildingEnvelope>(context), IBuildingEnvelopeRepository
    {
        public async Task<BuildingEnvelope[]> GetAllAsReadOnlyAsync(Guid? filterId = null, CancellationToken cancellationToken = default)
             => await context.BuildingEnvelopes
                .Where(p => p.RoomId == filterId)
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);

        public async Task<OuterWall?> GetWallByDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await context.BuildingEnvelopes
                .OfType<OuterWall>()
                .Where(x => x.RoomId == roomId && x.Direction == direction)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<bool> IsThereAWallOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await context.BuildingEnvelopes
                .OfType<OuterWall>()
                .AsNoTracking()
                .AnyAsync(x => x.RoomId == roomId && x.Direction == direction, cancellationToken);

        public async Task<bool> IsThereAnOpeningOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await context.BuildingEnvelopes
                .OfType<Opening>()
                .AsNoTracking()
                .AnyAsync(x => x.RoomId == roomId && x.Direction == direction, cancellationToken);

        public async Task<bool> IsThereAFloorInRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
            => await context.BuildingEnvelopes
                .OfType<Floor>()
                .AsNoTracking()
                .AnyAsync(x => x.RoomId == roomId, cancellationToken);

        public async Task<bool> IsThereARoofInRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
            => await context.BuildingEnvelopes
                .OfType<Roof>()
                .AsNoTracking()
                .AnyAsync(x => x.RoomId == roomId, cancellationToken);

        public async Task<long> GetInternalFencesCountByRoom(Guid roomId, CancellationToken cancellationToken = default)
            => await context.BuildingEnvelopes
                .OfType<InternalFence>()
                .AsNoTracking()
                .Where(x => x.RoomId == roomId)
                .SumAsync(x => x.Count, cancellationToken);

        public async Task<long> GetOpeningsCountByRoom(Guid roomId, CancellationToken cancellationToken = default)
            => await context.BuildingEnvelopes
                .OfType<Opening>()
                .AsNoTracking()
                .Where(x => x.RoomId == roomId)
                .SumAsync(x => x.Count, cancellationToken);
    }
}
