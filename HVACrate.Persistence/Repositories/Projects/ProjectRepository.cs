using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.Projects;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Projects
{
    public class ProjectRepository
        : BaseRepository<Project>, IProjectRepository
    {

        private readonly HVACrateDbContext _context;
        public ProjectRepository(HVACrateDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result<Project>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? searchId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Project> baseQuery = _context.Projects
                .Include(p => p.HVACUser)
                .Where(p => p.HVACUserId == searchId)
                .WithSearch(query.SearchParam, x => EF.Property<string>(x, query.QueryParam))
                .AsNoTracking();

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            Project[] projects = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<Project>(totalCount, projects);
        }

        public async Task<DateTimeOffset> GetLastTimeModifiedDateAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Projects
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => p.LastModified)
                .SingleOrDefaultAsync(cancellationToken);

        public async Task<string?> GetProjectNameAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return project?.Name;
        }
    }
}
