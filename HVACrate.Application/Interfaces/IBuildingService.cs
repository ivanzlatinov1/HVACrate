using HVACrate.Application.Models;
using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Interfaces;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    public interface IBuildingService : ICalculator
    {
        Task<Result<BuildingModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? projectId, CancellationToken cancellationToken = default);
        Task<BuildingModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BuildingModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(BuildingModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(BuildingModel model, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
