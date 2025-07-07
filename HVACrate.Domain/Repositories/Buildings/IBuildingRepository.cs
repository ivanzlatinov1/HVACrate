using HVACrate.Domain.Entities;

namespace HVACrate.Domain.Repositories.Buildings
{
    public interface IBuildingRepository : IBaseRepository<Building>, ICalculator
    {
    }
}
