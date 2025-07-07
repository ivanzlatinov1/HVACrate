using HVACrate.Domain.ValueObjects;

namespace HVACrate.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<Result<TEntity>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? creatorId = null, CancellationToken cancellationToken = default);
        Task<Result<TEntity>> GetAllAsync(BaseQuery query, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, CancellationToken cancellationToken);
        void SoftDelete(IDeletableModel entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
