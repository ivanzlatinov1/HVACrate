namespace HVACrate.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void SoftDelete(IDeletableModel entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
