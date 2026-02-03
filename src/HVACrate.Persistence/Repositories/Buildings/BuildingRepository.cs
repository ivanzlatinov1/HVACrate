using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.Buildings;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Buildings
{
    public class BuildingRepository : BaseRepository<Building>, IBuildingRepository
    {

        private readonly HVACrateDbContext _context;
        public BuildingRepository(HVACrateDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result<Building>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Building> baseQuery = _context.Buildings
                .Include(b => b.Project)
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

        public new async Task<Building> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Buildings
                    .Include(b => b.Project)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(b => b.Id == id, cancellationToken)
                    ?? throw new KeyNotFoundException($"Entity with id {id} cannot be found");

        public async Task<double> GetProjectRegionTemperature(Guid buildingId, CancellationToken cancellationToken = default)
        {
            var building = await this.GetByIdAsReadOnlyAsync(buildingId, cancellationToken);

            return building.Project.RegionTemperature;
        }
    }
}
