using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HVACrate.Data.Models
{
    public class Building
    {
        [Comment("Building's unique identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Name of the building")]
        public string Name { get; set; } = null!;

        [Comment("Building's geographical placement")]
        public string Location { get; set; } = null!;

        [Comment("Total height of the building")]
        public double TotalHeight { get; set; }

        [Comment("Wind speed used for infiltration")]
        public double WindSpeed { get; set; }

        [Comment("Reference to the project this building belongs to")]
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;

        [Comment("An image of the building")]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = [];
    }
}
