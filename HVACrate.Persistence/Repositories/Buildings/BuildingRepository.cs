using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.Buildings;
using HVACrate.Domain.Repositories.Rooms;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Buildings
{
    public class BuildingRepository(HVACrateDbContext context, IRoomRepository roomRepository) 
        : BaseRepository<Building>(context), IBuildingRepository
    {
        private readonly IRoomRepository _roomRepository = roomRepository;

        public override async Task<Result<Building>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Building> baseQuery = context.Buildings
                .Where (p => p.ProjectId == filterId)
                .WithSearch(query.SearchParam, x => EF.Property<string>(x, query.QueryParam))
                .AsNoTracking();

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            Building[] buildings = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<Building>(totalCount, buildings);
        }
        public async Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken)
        {
            Building building = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double totalHeatInfiltration = 0;

            foreach(var room in building.Rooms)
            {
                totalHeatInfiltration += 
                    await this._roomRepository.CalculateTotalHeatInfiltration(room.Id, cancellationToken);
            }

            return totalHeatInfiltration;
        }

        public async Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken)
        {
            Building building = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double totalHeatTransmission = 0;

            foreach(var room in building.Rooms)
            {
                totalHeatTransmission += 
                    await this._roomRepository.CalculateTotalHeatTransmission(room.Id, cancellationToken);
            }

            return totalHeatTransmission;
        }
    }
}
