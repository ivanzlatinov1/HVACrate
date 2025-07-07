namespace HVACrate.Domain.Entities.BuildingEnvelopes
{
    public class Opening : BuildingEnvelope
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.WindowsAndDoors;

        public Direction Direction { get; set; }

        public double JointLength { get; set; }

        public double VentilationCoefficient { get; set; }
    }
}
