namespace HVACrate.Data.Models
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public double RegionTemperature { get; set; }

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public virtual ICollection<Building> Buildings { get; set; } = [];
    }
}
