using HVACrate.Application.Models.Projects;
using HVACrate.Application.Models.Rooms;

namespace HVACrate.Application.Models.Buildings
{
    public class BuildingModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public double TotalHeight { get; set; }

        public double WindSpeed { get; set; }

        public int Floors { get; set; }

        public string Orientation { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public Guid ProjectId { get; set; }

        public virtual ProjectModel Project { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public virtual ICollection<RoomModel> Rooms { get; set; } = [];
    }
}
