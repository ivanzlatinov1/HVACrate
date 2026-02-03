using HVACrate.Domain.Enums;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class RoofModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.Roof;
    }
}
