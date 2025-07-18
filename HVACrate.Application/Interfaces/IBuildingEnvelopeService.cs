using HVACrate.Application.Models;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    public interface IBuildingEnvelopeService
    {
        Task<Result<BuildingEnvelopeModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? buildingId, CancellationToken cancellationToken = default);
        Task<BuildingEnvelopeModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BuildingEnvelopeModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope);
        double CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope);
    }
}
