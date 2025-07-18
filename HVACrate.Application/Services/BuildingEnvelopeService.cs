using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Enums;

namespace HVACrate.Application.Services
{
    internal class BuildingEnvelopeService : IBuildingEnvelopeService
    {
        public double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope)
        {
            throw new NotImplementedException();
        }

        public double CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope)
        {
            double area = buildingEnvelope.Width * buildingEnvelope.Height;
            double thermalResistance = 1.0 / buildingEnvelope.HeatTransferCoefficient;
            double roomTemperature = buildingEnvelope.Room.Temperature;
            double regionTemperature = buildingEnvelope.Room.Building.Project.RegionTemperature;
            double adjustedTemperature = buildingEnvelope.AdjustedTemperature;
            double orientationCoefficient = 1.0;

            if (buildingEnvelope is OuterWallModel outerWall)
            {
                orientationCoefficient = GetOrientationCoefficient(outerWall.Direction);
            }
            else if (buildingEnvelope is OpeningModel opening)
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
