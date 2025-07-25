using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.Buildings;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Buildings
{
    public class BuildingRepository(HVACrateDbContext context) : BaseRepository<Building>(context), IBuildingRepository
    {

        public async Task<Result<Building>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Building> baseQuery = context.Buildings
                .Where(p => p.ProjectId == filterId)
                .WithSearch(query.SearchParam, x => EF.Property<string>(x, query.QueryParam))
                .AsNoTracking();

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            Building[] buildings = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<Building>(totalCount, buildings);
        }

        public async Task<double> GetProjectRegionTemperature(Guid buildingId, CancellationToken cancellationToken = default)
        {
            var building = await this.GetByIdAsReadOnlyAsync(buildingId, cancellationToken);

            return building.Project.RegionTemperature;
        }
    }
}
