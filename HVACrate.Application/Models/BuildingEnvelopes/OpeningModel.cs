using HVACrate.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class OpeningModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.WindowsAndDoors;

        public Direction Direction { get; set; }

        [Required(ErrorMessage = Required)]
        public double JointLength { get; set; }

        [Required(ErrorMessage = Required)]
        public double VentilationCoefficient { get; set; }

        [Required(ErrorMessage = Required)]
        public int Count { get; set; }
    }
}
