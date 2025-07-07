using HVACrate.Domain.Interfaces;
using HVACrate.Domain.Repositories;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity>(HVACrateDbContext context) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly HVACrateDbContext _context = context;

        public virtual async Task<Result<TEntity>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? creatorId = null, CancellationToken cancellationToken = default)
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

        public async Task<Result<TEntity>> GetAllAsync(BaseQuery query, CancellationToken cancellationToken = default)
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

        public async Task UpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            TEntity entity = await this.GetByIdAsync(id, cancellationToken);
            this._context.Set<TEntity>().Update(entity);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public void SoftDelete(IDeletableModel entity)
        {
            entity.IsDeleted = true;
        }

    }
}
