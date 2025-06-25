using HVACrate.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Data.Models
{
    public class Material
    {
        [Comment("Material's unique identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Material's type, e.g., Brick, Concrete")]
        public MaterialType Type { get; set; }

        [Comment("Material's thermal conductivity in W/mK")]
        public double ThermalConductivity { get; set; }

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
