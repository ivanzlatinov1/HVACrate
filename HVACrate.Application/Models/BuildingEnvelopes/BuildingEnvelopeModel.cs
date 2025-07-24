using HVACrate.Application.Models.Materials;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Enums;
using System.ComponentModel.DataAnnotations;

using static HVACrate.Domain.ValidationConstants.BuildingEnvelope;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;
using static HVACrate.Domain.ValidationConstants.ValidationMessages.BuildingEnvelopeValidationMessages;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public abstract class BuildingEnvelopeModel
    {
        [Required]
        public Guid Id { get; set; }

        public virtual BuildingEnvelopeType Type { get; protected set; }

        [Required(ErrorMessage = Required)]
        [Range(MinHeight, MaxHeight, ErrorMessage = HeightRange)]
        public double Height { get; set; }

        [Required(ErrorMessage = Required)]
        [Range(MinWidth, MaxWidth, ErrorMessage = WidthRange)]
        public double Width { get; set; }

        [Required(ErrorMessage = Required)]
        [Range(MinDensity, MaxDensity, ErrorMessage = DensityRange)]
        public double Density { get; set; }

        [Required(ErrorMessage = Required)]
        [Range(MinHeatTransferCoefficient, MaxHeatTransferCoefficient, ErrorMessage = HeatTransferCoefficientRange)]
        public double HeatTransferCoefficient { get; set; }

        public double AdjustedTemperature { get; set; }

        public double Area { get; set; }

        public bool IsDeleted { get; set; }

        public Guid RoomId { get; set; }

        public virtual RoomModel Room { get; set; } = null!;

        public Guid MaterialId { get; set; }

        public virtual MaterialModel Material { get; set; } = null!;
    }
}
