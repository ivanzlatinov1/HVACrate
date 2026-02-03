using HVACrate.Domain.Repositories.Materials;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Materials
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {

        private readonly HVACrateDbContext _context;
        public MaterialRepository(HVACrateDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Material[]> GetAllAsReadOnlyAsync(CancellationToken cancellationToken = default)
            => await _context.Materials
                    .AsNoTracking()
                    .ToArrayAsync(cancellationToken);

        public async Task<bool> CheckIfMaterialWithSameTypeExistsAsync(string type, CancellationToken cancellationToken = default)
            => await _context.Materials
                   .AsNoTracking()
                   .AnyAsync(x => x.Type == type, cancellationToken);
    }
}
