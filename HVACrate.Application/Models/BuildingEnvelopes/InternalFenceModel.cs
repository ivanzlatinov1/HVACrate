using HVACrate.Domain.Enums;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class InternalFenceModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.InternalFence;
    }
}
