using Microsoft.EntityFrameworkCore;

namespace HVACrate.Data.Models
{
    public class Project
    {
        [Comment("Project's unique identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Name of the project")]
        public string Name { get; set; } = null!;

        [Comment("Average external temperature of region")]
        public double RegionTemperature { get; set; }

        [Comment("The date when the project was created")]
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public virtual ICollection<Building> Buildings { get; set; } = [];
    }
}
