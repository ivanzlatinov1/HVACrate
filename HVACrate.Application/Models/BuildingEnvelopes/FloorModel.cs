using HVACrate.Domain.Enums;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class FloorModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.Floor;

        public double ThermalConductivityResistance { get; set; }

        public double GroundWaterLength { get; set; }

        public double GroundWaterTemperature { get; set; }
    }
}
