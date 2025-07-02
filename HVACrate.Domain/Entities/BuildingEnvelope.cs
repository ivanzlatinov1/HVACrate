namespace HVACrate.Domain.Entities
{
    public class BuildingEnvelope : IDeletableModel
    {
        public Guid Id { get; set; }

        public BuildingEnvelopeType Type { get; set; }

        public Direction Direction { get; set; }

        public double ZOrientationCoefficient { get; set; }

        public bool IsDeleted { get; set; }

        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;

        public Guid MaterialId { get; set; }

        public virtual Material Material { get; set; } = null!;
    }
}
