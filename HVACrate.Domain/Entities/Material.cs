namespace HVACrate.Domain.Entities
{
    public class Material : BaseEntity, IDeletableModel
    {
        public string Type { get; set; } = null!;

        public double ThermalConductivity { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = new List<BuildingEnvelope>();
    }
}
