using HVACrate.Data.Models.Interfaces;

namespace HVACrate.Data.Models
{
    public class Material : IDeletableModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public double ThermalConductivity { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
