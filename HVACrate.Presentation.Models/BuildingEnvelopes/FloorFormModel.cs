namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public class FloorFormModel : BuildingEnvelopeFormModel
    {
        public override string Type => "Floor";

        public double ThermalConductivityResistance { get; set; }

        public double GroundWaterLength { get; set; }

        public double GroundWaterTemperature { get; set; }
    }
}
