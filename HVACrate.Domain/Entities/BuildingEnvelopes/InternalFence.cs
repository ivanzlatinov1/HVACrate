
namespace HVACrate.Domain.Entities.BuildingEnvelopes
{
    public class InternalFence : BuildingEnvelope
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.InternalFence;

        public int Count { get; set; }
    }
}
