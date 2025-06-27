using HVACrate.Data.Models.Interfaces;

namespace HVACrate.Data.Models
{
    public class Room : IDeletableModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public string? Number { get; set; }

        public double Temperature { get; set; }

        public double HeatLossTransmission { get; set; }

        public double HeatLossInfiltration { get; set; }

        public bool IsDeleted { get; set; }

        public Guid BuildingId { get; set; }

        public virtual Building Building { get; set; } = null!;

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
