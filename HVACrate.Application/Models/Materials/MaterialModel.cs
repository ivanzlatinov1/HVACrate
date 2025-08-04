using HVACrate.Application.Models.BuildingEnvelopes;
using System.ComponentModel.DataAnnotations;
using static HVACrate.Domain.ValidationConstants.Material;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;
using static HVACrate.Domain.ValidationConstants.ValidationMessages.BuildingEnvelopeValidationMessages;

namespace HVACrate.Application.Models.Materials
{
    public class MaterialModel
    {
        [Required(ErrorMessage = MaterialRequired)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = Required)]
        [StringLength(TypeMaxLength, MinimumLength = TypeMinLength, ErrorMessage = StringLength)]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = Required)]
        public double ThermalConductivity { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BuildingEnvelopeModel> BuildingEnvelopes { get; set; } = [];
    }
}
