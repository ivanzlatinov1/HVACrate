
namespace HVACrate.Domain.Entities.BuildingEnvelopes
{
    public class OuterWall : BuildingEnvelope
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.OuterWall;

        public Direction Direction { get; set; }

        public double AdjustedTemperature { get; set; }

        public bool ShouldReduceHeatingArea { get; set; }

    }
}
