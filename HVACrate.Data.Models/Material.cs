using HVACrate.Data.Models.Enums;

namespace HVACrate.Data.Models
{
    public class Material
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public MaterialType Type { get; set; }

        public double ThermalConductivity { get; set; }

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
