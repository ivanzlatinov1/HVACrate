using HVACrate.Domain.Entities.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.BuildingEnvelopes;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.BuildingEnvelopes
{
    public class BuildingEnvelopeRepository(HVACrateDbContext context)
        : BaseRepository<BuildingEnvelope>(context), IBuildingEnvelopeRepository
    {
        public override async Task<Result<BuildingEnvelope>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<BuildingEnvelope> baseQuery = context.BuildingsEnvelope
                .Where(p => p.RoomId == filterId)
                .AsNoTracking();

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            BuildingEnvelope[] buildingEnvelopes = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<BuildingEnvelope>(totalCount, buildingEnvelopes);
        }

        public async Task<OuterWall?> GetWallByDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await context.BuildingsEnvelope
                .OfType<OuterWall>()
                .Where(x => x.RoomId == roomId && x.Type == BuildingEnvelopeType.OuterWall && x.Direction == direction)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<bool> IsThereAWallOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await context.BuildingsEnvelope
                .OfType<OuterWall>()
                .AnyAsync(x => x.RoomId == roomId && x.Direction == direction, cancellationToken);

        public async Task<bool> IsThereAnOpeningOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await context.BuildingsEnvelope
                .OfType<Opening>()
                .AnyAsync(x => x.RoomId == roomId && x.Direction == direction, cancellationToken);

        public async Task<bool> IsThereAFloorInRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
            => await context.BuildingsEnvelope
                .AnyAsync(x => x.RoomId == roomId && x.Type == BuildingEnvelopeType.Floor, cancellationToken);

        public async Task<bool> IsThereARoofInRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
            => await context.BuildingsEnvelope
                .AnyAsync(x => x.RoomId == roomId && x.Type == BuildingEnvelopeType.Roof, cancellationToken);
    }
}
