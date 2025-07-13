using HVACrate.Domain.Enums;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class OuterWallModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.OuterWall;

        public Direction Direction { get; set; }

        public bool ShouldReduceHeatingArea { get; set; }
    }
}
