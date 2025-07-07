using HVACrate.Domain.Entities.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Domain.Repositories.BuildingEnvelopes;

namespace HVACrate.Persistence.Repositories.BuildingEnvelopes
{
    public class BuildingEnvelopeRepository(HVACrateDbContext context)
        : BaseRepository<BuildingEnvelope>(context), IBuildingEnvelopeRepository
    {
        public double CalculateHeatInfiltration(BuildingEnvelope buildingEnvelope)
        {
            throw new NotImplementedException();
        }

        public double CalculateHeatTransmission(BuildingEnvelope buildingEnvelope)
        {
            double area = buildingEnvelope.Width * buildingEnvelope.Height;
            double thermalResistance = 1.0 / buildingEnvelope.HeatTransferCoefficient;
            double roomTemperature = buildingEnvelope.Room.Temperature;
            double regionTemperature = buildingEnvelope.Room.Building.Project.RegionTemperature;
            double adjustedTemperature = buildingEnvelope.AdjustedTemperature;
            double orientationCoefficient = 1.0;

            if (buildingEnvelope is OuterWall outerWall)
            {
                orientationCoefficient = GetOrientationCoefficient(outerWall.Direction);
            }
            else if (buildingEnvelope is Opening opening)
            {
                orientationCoefficient = GetOrientationCoefficient(opening.Direction);
            }

            return area / thermalResistance *
                (roomTemperature - regionTemperature - adjustedTemperature) * orientationCoefficient;
        }

        private static double GetOrientationCoefficient(Direction direction)
        {
            return direction switch
            {
                Direction.North or Direction.East or Direction.Northeast or Direction.Northwest => 1.10,
                Direction.South or Direction.Southwest => 1.0,
                Direction.West or Direction.Southeast => 1.05,
                _ => 1.0,
            };
        }
    }
}
