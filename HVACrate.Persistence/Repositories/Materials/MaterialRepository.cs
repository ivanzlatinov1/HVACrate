using HVACrate.Domain.Repositories.Materials;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Materials
{
    public class MaterialRepository(HVACrateDbContext context) : BaseRepository<Material>(context), IMaterialRepository
    {
        public async Task<Material[]> GetAllAsReadOnlyAsync(CancellationToken cancellationToken = default)
            => await context.Materials
                    .AsNoTracking()
                    .ToArrayAsync(cancellationToken);

        public async Task<bool> CheckIfMaterialWithSameTypeExistsAsync(string type, CancellationToken cancellationToken = default)
            => await context.Materials
                   .AsNoTracking()
                   .AnyAsync(x => x.Type == type, cancellationToken);
    }
}
