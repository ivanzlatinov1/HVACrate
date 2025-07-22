using HVACrate.Domain.Enums;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class OpeningModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.WindowsAndDoors;

        public Direction Direction { get; set; }

        public double JointLength { get; set; }

        public double VentilationCoefficient { get; set; }

        public int Count { get; set; }
    }
}
