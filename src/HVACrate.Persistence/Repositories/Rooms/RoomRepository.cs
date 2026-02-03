using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.Rooms;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Rooms
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {

        private readonly HVACrateDbContext _context;
        public RoomRepository(HVACrateDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result<Room>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Room> baseQuery = _context.Rooms
                .Include(r => r.Building)
                .Where(r => r.BuildingId == filterId)
                .AsNoTracking();

            if (query.SearchParam != null)
            {
                baseQuery = baseQuery
                    .Where(r => r.Floor == int.Parse(query.SearchParam))
                    .AsNoTracking();
            }

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            Room[] rooms = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<Room>(totalCount, rooms);
        }

        public async Task<Room[]> GetAllEnclosedRoomsAsync(Pagination pagination, CancellationToken cancellationToken = default)
            => await _context.Rooms
                .Include(r => r.Building)
                .Include(r => r.BuildingEnvelopes)
                .Where(x => x.IsEnclosed)
                .WithPagination(pagination)
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);
    }
}
