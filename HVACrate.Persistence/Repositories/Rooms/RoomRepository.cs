using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.BuildingEnvelopes;
using HVACrate.Domain.Repositories.Rooms;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Rooms
{
    public class RoomRepository(HVACrateDbContext context, IBuildingEnvelopeRepository
        buildingEnvelopeRepository): BaseRepository<Room>(context), IRoomRepository
    {
        private readonly IBuildingEnvelopeRepository _buildingEnvelopeRepository = buildingEnvelopeRepository;

        public override async Task<Result<Room>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Room> baseQuery = context.Rooms
                .Where (p => p.BuildingId == filterId)
                .WithSearch(query.SearchParam, x => EF.Property<string>(x, query.QueryParam))
                .AsNoTracking();

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            Room[] rooms = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<Room>(totalCount, rooms);
        }

        public async Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken)
        {
            Room room = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double heatInfiltration = 0;
            foreach(var buildingEnvelope in room.BuildingEnvelopes)
            {
                heatInfiltration += this._buildingEnvelopeRepository
                    .CalculateHeatInfiltration(buildingEnvelope);
            }

            return heatInfiltration;
        }

        public async Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken)
        {
            Room room = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double heatTransmission = 0;
            foreach(var buildingEnvelope in room.BuildingEnvelopes)
            {
                heatTransmission += this._buildingEnvelopeRepository
                    .CalculateHeatTransmission(buildingEnvelope);
            }

            return heatTransmission;
        }
    }
}
