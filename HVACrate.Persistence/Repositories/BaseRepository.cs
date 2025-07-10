using HVACrate.Domain.Interfaces;
using HVACrate.Domain.Repositories;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity>(HVACrateDbContext dbContext) : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly HVACrateDbContext _context = dbContext;

        public virtual async Task<Result<TEntity>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> baseQuery = _context
                .Set<TEntity>()
                .WithSearch(query.SearchParam, x => EF.Property<string>(x, query.QueryParam))
                .AsNoTracking();

            int totalCount = await baseQuery
                .CountAsync(cancellationToken);

            TEntity[] entities = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<TEntity>(totalCount, entities);
        }

        public virtual async Task<Result<TEntity>> GetAllAsync(BaseQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> baseQuery = _context
                .Set<TEntity>()
                .WithSearch(query.SearchParam, x => EF.Property<string>(x, query.QueryParam));

            int count = await baseQuery
                .CountAsync(cancellationToken);

            TEntity[] entities = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken);

            return new Result<TEntity>(count, entities);
        }

        public async Task<TEntity> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>()
                    .AsNoTracking()
                    .SingleAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken)
                    ?? throw new KeyNotFoundException($"Entity with id {id} cannot be found");

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>()
                .SingleAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken)
                ?? throw new KeyNotFoundException($"Entity with id {id} cannot be found");

        public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this._context.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            var tracked = this._context.Set<TEntity>().Local.FirstOrDefault(e => e.Id == entity.Id);
            if (tracked != null)
            {
                this._context.Entry(tracked).CurrentValues.SetValues(entity);
            }
            else
            {
                this._context.Set<TEntity>().Update(entity);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetLastModifiedTimestamps();
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public void SoftDelete(IDeletableModel entity)
        {
            entity.IsDeleted = true;
        }

        // Helper method to track when an entity is updated and to update its project's LastModified property
        private void SetLastModifiedTimestamps()
        {
            HashSet<Project> modifiedProjects = [];

            var modifiedEntries = _context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
                .ToList();

            foreach (var entry in modifiedEntries)
            {
                Project? project = GetRelatedProject(entry.Entity);
                if (project == null) continue;

                var projectEntry = _context.ChangeTracker
                    .Entries<Project>()
                    .FirstOrDefault(e => e.Entity == project);

                if (projectEntry == null)
                {
                    _context.Attach(project);
                    projectEntry = _context.Entry(project);
                    projectEntry.State = EntityState.Modified;
                }
                else if (projectEntry.State == EntityState.Unchanged)
                {
                    projectEntry.State = EntityState.Modified;
                }

                modifiedProjects.Add(project);
            }

            foreach (var project in modifiedProjects)
            {
                project.LastModified = DateTimeOffset.UtcNow;
            }
        }

        private static Project? GetRelatedProject(BaseEntity entity)
        {
            return entity switch
            {
                Project project => project,
                Building building => building.Project,
                Room room => room.Building?.Project,
                BuildingEnvelope envelope => envelope.Room?.Building?.Project,
                _ => null
            };
        }
    }
}
