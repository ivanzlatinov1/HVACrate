namespace HVACrate.Domain.Entities
{
    public abstract class BuildingEnvelope : BaseEntity, IDeletableModel
    {
        public virtual BuildingEnvelopeType Type { get; protected set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Density { get; set; }

        public int Count { get; set; }

        public double AdjustedTemperature { get; set; }

        public double HeatTransferCoefficient { get; set; }

        public bool IsDeleted { get; set; }

        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;

        public Guid MaterialId { get; set; }

        public virtual Material Material { get; set; } = null!;
    }
}
