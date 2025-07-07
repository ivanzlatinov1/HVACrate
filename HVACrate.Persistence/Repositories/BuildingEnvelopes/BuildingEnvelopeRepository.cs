using HVACrate.Domain.Repositories.BuildingEnvelopes;

namespace HVACrate.Persistence.Repositories.BuildingEnvelopes
{
    public class BuildingEnvelopeRepository(HVACrateDbContext context)
        : BaseRepository<BuildingEnvelope>(context), IBuildingEnvelopeRepository
    {
        public double CalculateHeatInfiltration(BuildingEnvelope buildingEnvelope, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public double CalculateHeatTransmission(BuildingEnvelope buildingEnvelope, CancellationToken cancellation)
        {
            double area = buildingEnvelope.Width * buildingEnvelope.Height;
            double thermalResistance = 1 / buildingEnvelope.HeatTransferCoefficient;
            double roomTemperature = buildingEnvelope.Room.Temperature;
            double regionTemperature = buildingEnvelope.Room.Building.Project.RegionTemperature;
            double adjustedTemperature = buildingEnvelope.AdjustedTemperature;
            double orientationCoefficient = buildingEnvelope.ZOrientationCoefficient;

            return area / thermalResistance * 
                (roomTemperature - regionTemperature - adjustedTemperature) * orientationCoefficient;
        }
    }
}
