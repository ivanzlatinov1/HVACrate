using Microsoft.EntityFrameworkCore;

namespace HVACrate.Data.Models
{
    public class Room
    {
        [Comment("Room's unique identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("The type of the room, e.g., Bathroom, Bedroom")]
        public string Type { get; set; } = null!;

        [Comment("Number of the room, e.g., 101, 102")]
        public string? Number { get; set; }

        [Comment("Internal room temperature")]
        public double Temperature { get; set; }

        [Comment("Calculated heat loss from transmission")]
        public float HeatLossTransmission { get; set; }

        [Comment("Calculated heat loss from infiltration")]
        public float HeatLossInfiltration { get; set; }

        [Comment("Reference to the building this room belongs to")]
        public Guid BuildingId { get; set; }

        public virtual Building Building { get; set; } = null!;

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
