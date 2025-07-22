using HVACrate.Application.Models.BuildingEnvelopes;

namespace HVACrate.Application.Models.Materials
{
    public class MaterialModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public double ThermalConductivity { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BuildingEnvelopeModel> BuildingEnvelopes { get; set; } = [];
    }
}
