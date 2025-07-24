using HVACrate.Application.Models.Projects;
using HVACrate.Application.Models.Rooms;
using System.ComponentModel.DataAnnotations;

using static HVACrate.Domain.ValidationConstants.Building;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;

namespace HVACrate.Application.Models.Buildings
{
    public class BuildingModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = Required)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = StringLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = Required)]
        [StringLength(LocationMaxLength, MinimumLength = LocationMinLength, ErrorMessage = StringLength)]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = Required)]
        public double TotalHeight { get; set; }

        [Required(ErrorMessage = Required)]
        public double WindSpeed { get; set; }

        [Required(ErrorMessage = Required)]
        public int Floors { get; set; }

        [Required(ErrorMessage = Required)]
        public string Orientation { get; set; } = null!;

        [Url(ErrorMessage = InvalidUrl)]
        public string? ImageUrl { get; set; }

        public Guid ProjectId { get; set; }

        public virtual ProjectModel Project { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public virtual ICollection<RoomModel> Rooms { get; set; } = [];
    }
}
