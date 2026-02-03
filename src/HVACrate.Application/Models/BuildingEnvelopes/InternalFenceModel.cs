using HVACrate.Domain.Enums;
using System.ComponentModel.DataAnnotations;

using static HVACrate.Domain.ValidationConstants.ValidationMessages;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public class InternalFenceModel : BuildingEnvelopeModel
    {
        public override BuildingEnvelopeType Type => BuildingEnvelopeType.InternalFence;

        [Required(ErrorMessage = Required)]
        public int Count { get; set; }

        [Required(ErrorMessage = Required)]
        public int AdjacentRoomTemperature { get; set; }
    }
}
