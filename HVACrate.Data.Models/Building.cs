using Microsoft.AspNetCore.Http;

namespace HVACrate.Data.Models
{
    public class Building
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public double TotalHeight { get; set; }

        public double WindSpeed { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = [];
    }
}
