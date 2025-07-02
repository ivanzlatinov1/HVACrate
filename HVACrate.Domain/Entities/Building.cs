namespace HVACrate.Domain.Entities
{
    public class Building : IDeletableModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public double TotalHeight { get; set; }

        public double WindSpeed { get; set; }

        public bool IsDeleted { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = [];
    }
}
