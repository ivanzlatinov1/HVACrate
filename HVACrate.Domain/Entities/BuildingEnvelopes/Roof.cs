namespace HVACrate.Domain.Entities.BuildingEnvelopes
{
    public class Roof : BuildingEnvelope
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.Roof;

        public double AdjustedTemperature { get; set; }
    }
}
