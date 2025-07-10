using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.Projects;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Projects
{
    public class ProjectRepository(HVACrateDbContext context)
        : BaseRepository<Project>(context), IProjectRepository
    {
        public override async Task<Result<Project>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? searchId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Project> baseQuery = context.Projects
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
            => await context.Projects
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => p.LastModified)
                .SingleOrDefaultAsync(cancellationToken);
    }
}
