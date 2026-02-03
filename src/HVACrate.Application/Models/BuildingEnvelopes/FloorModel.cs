using HVACrate.Domain.Enums;
using System.ComponentModel.DataAnnotations;

using static HVACrate.Domain.ValidationConstants.BuildingEnvelope;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class FloorModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.Floor;

        [Required(ErrorMessage = Required)]
        public double ThermalConductivityResistance { get; set; }

        [Required(ErrorMessage = Required)]
        public double GroundWaterLength { get; set; }

        [Required(ErrorMessage = Required)]
        [Range(MinTemperature, MaxTemperature, ErrorMessage = TemperatureRange)]
        public double GroundWaterTemperature { get; set; }
    }
}
