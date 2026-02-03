namespace HVACrate.Domain.Entities.BuildingEnvelopes
{
    public class Floor : BuildingEnvelope
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.Floor;

        public double ThermalConductivityResistance { get; set; }

        public double GroundWaterLength { get; set; }

        public double GroundWaterTemperature { get; set; }
    }
}
