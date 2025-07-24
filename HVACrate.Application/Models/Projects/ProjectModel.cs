using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Entities;
using System.ComponentModel.DataAnnotations;

using static HVACrate.Domain.ValidationConstants.Project;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;

namespace HVACrate.Application.Models.Projects
{
    public class ProjectModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = Required)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = StringLength)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = Required)]
        [Range(MinRegionTemperature, MaxRegionTemperature, ErrorMessage = TemperatureRange)]
        public double RegionTemperature { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public Guid HVACUserId { get; set; }

        public virtual HVACUser HVACUser { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public virtual ICollection<BuildingModel> Buildings { get; set; } = [];
    }
}
