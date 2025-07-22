using HVACrate.Domain.Interfaces;
using HVACrate.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity>(HVACrateDbContext dbContext) : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly HVACrateDbContext _context = dbContext;

        public async Task<TEntity> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken)
                    ?? throw new KeyNotFoundException($"Entity with id {id} cannot be found");

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>()
                .SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken)
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
            await this.SetLastModifiedTimestamps();
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public void SoftDelete(IDeletableModel entity)
        {
            entity.IsDeleted = true;
        }

        // Helper method to track when an entity is updated and to update its project's LastModified property
        private async Task SetLastModifiedTimestamps()
        {
            HashSet<Project> modifiedProjects = [];

            var modifiedEntries = _context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
                .ToList();

            foreach (var entry in modifiedEntries)
            {
                Project? project = await GetRelatedProjectAsync(entry.Entity);
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

        private async Task<Project?> GetRelatedProjectAsync(BaseEntity entity) => entity switch
        {
            Project project => project,

            Building building => await QueryProjectAsync(building.ProjectId),

            Room room => await QueryProjectAsync(id: (await QueryBuildingAsync(room.BuildingId))?.ProjectId),

            BuildingEnvelope envelope => await QueryProjectAsync(
                id: (await QueryBuildingAsync(
                    id: (await QueryRoomAsync(envelope.RoomId))?.BuildingId
                    ))?.ProjectId
                ),

            _ => null
        };

        private async Task<Project?> QueryProjectAsync(Guid? id) => await this._context.Projects.FindAsync(id);
        private async Task<Building?> QueryBuildingAsync(Guid? id) => await this._context.Buildings.FindAsync(id);
        private async Task<Room?> QueryRoomAsync(Guid? id) => await this._context.Rooms.FindAsync(id);
    }
}
