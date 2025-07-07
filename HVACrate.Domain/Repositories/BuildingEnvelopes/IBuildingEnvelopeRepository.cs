using HVACrate.Domain.Entities;

namespace HVACrate.Domain.Repositories.BuildingEnvelopes
{
    public interface IBuildingEnvelopeRepository : IBaseRepository<BuildingEnvelope>
    {
        double CalculateHeatInfiltration(BuildingEnvelope buildingEnvelope);
        double CalculateHeatTransmission(BuildingEnvelope buildingEnvelope);
    }
}
