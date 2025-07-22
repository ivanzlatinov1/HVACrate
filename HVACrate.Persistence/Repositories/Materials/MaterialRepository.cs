using HVACrate.Domain.Repositories.Materials;

namespace HVACrate.Persistence.Repositories.Materials
{
    public class MaterialRepository(HVACrateDbContext context) : BaseRepository<Material>(context), IMaterialRepository
    {
    }
}
