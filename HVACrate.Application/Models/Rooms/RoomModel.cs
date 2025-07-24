using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Application.Models.Buildings;
using System.ComponentModel.DataAnnotations;

using static HVACrate.Domain.ValidationConstants.Room;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;

namespace HVACrate.Application.Models.Rooms
{
    public class RoomModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = Required)]
        [StringLength(TypeMaxLength, MinimumLength = TypeMinLength, ErrorMessage = StringLength)]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = Required)]
        [StringLength(NumberMaxLength, MinimumLength = NumberMinLength, ErrorMessage = StringLength)]
        public string Number { get; set; } = null!;

        [Required(ErrorMessage = Required)]
        [Range(MinTemperature, MaxTemperature, ErrorMessage = TemperatureRange)]
        public double Temperature { get; set; }

        [Required(ErrorMessage = Required)]
        public int Floor { get; set; }

        public bool IsEnclosed { get; set; }

        public bool IsDeleted { get; set; }

        public Guid BuildingId { get; set; }

        public virtual BuildingModel Building { get; set; } = null!;

        public virtual ICollection<BuildingEnvelopeModel> BuildingEnvelopes { get; set; } = [];
    }
}
