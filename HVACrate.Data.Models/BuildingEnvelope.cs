using HVACrate.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Data.Models
{
    public class BuildingEnvelope
    {
        [Comment("Building envelope's unique identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Type of the building envelope, e.g., Wall, Roof, Floor")]
        public BuildingEnvelopeType Type { get; set; }

        [Comment("The direction of the building envelope, e.g., North, East")]
        public Direction Direction { get; set; }

        [Comment("Orientation coefficient (Zo)")]
        public double ZOrientationCoefficient { get; set; }

        [Comment("Reference to the room this building envelope belongs to")]
        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;

        [Comment("Reference to the material of the building envelope")]
        public Guid MaterialId { get; set; }

        public virtual Material Material { get; set; } = null!;
    }
}
