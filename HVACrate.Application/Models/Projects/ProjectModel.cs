using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Models.Projects
{
    public class ProjectModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double RegionTemperature { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public Guid HVACUserId { get; set; }

        public virtual HVACUser HVACUser { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public virtual ICollection<BuildingModel> Buildings { get; set; } = [];
    }
}
