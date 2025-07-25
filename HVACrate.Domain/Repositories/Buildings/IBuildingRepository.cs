using HVACrate.Domain.Entities;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Domain.Repositories.Buildings
{
    public interface IBuildingRepository : IBaseRepository<Building>
    {
        Task<Result<Building>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default);
        Task<double> GetProjectRegionTemperature(Guid buildingId, CancellationToken cancellationToken = default);
    }
}
