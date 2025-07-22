using HVACrate.Application.Models.Materials;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Enums;

namespace HVACrate.Application.Models.BuildingEnvelopes
{
    public abstract class BuildingEnvelopeModel
    {
        public Guid Id { get; set; }

        public virtual BuildingEnvelopeType Type { get; protected set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Area { get; set; }

        public double Density { get; set; }

        public int Count { get; set; }

        public double AdjustedTemperature { get; set; }

        public double HeatTransferCoefficient { get; set; }

        public bool IsDeleted { get; set; }

        public Guid RoomId { get; set; }

        public virtual RoomModel Room { get; set; } = null!;

        public Guid MaterialId { get; set; }

        public virtual MaterialModel Material { get; set; } = null!;
    }
}
