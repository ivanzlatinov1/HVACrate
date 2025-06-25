namespace HVACrate.Data.Models
{
    public class Material : DeletableModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public double ThermalConductivity { get; set; }

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
