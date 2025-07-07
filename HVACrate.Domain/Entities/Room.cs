namespace HVACrate.Domain.Entities
{
    public class Room : BaseEntity, IDeletableModel
    {

        public string Type { get; set; } = null!;

        public string? Number { get; set; }

        public double Temperature { get; set; }

        public int Floor { get; set; }

        public bool IsDeleted { get; set; }

        public Guid BuildingId { get; set; }

        public virtual Building Building { get; set; } = null!;

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
