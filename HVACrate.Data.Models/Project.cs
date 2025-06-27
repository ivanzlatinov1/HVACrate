using HVACrate.Data.Models.Interfaces;

namespace HVACrate.Data.Models
{
    public class Project : IDeletableModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public double RegionTemperature { get; set; }

        public DateOnly CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Building> Buildings { get; set; } = [];
    }
}
