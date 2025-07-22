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
                .Where (p => p.RoomId == filterId)
                .AsNoTracking();

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            BuildingEnvelope[] buildingEnvelopes = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<BuildingEnvelope>(totalCount, buildingEnvelopes);
        }
    }
}
