using HVACrate.Application.Models.BuildingEnvelopes;

namespace HVACrate.Application.Interfaces
{
    public interface IBuildingEnvelopeService
    {
        double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope);
        double CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope);
    }
}
