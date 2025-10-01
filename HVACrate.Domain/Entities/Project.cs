namespace HVACrate.Domain.Entities
{
    public class Project : BaseEntity, IDeletableModel
    {

        public string Name { get; set; } = null!;

        public double RegionTemperature { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;

        public bool IsDeleted { get; set; }

        public Guid HVACUserId { get; set; }

        public virtual HVACUser HVACUser { get; set; } = null!;

        public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
    }
}
