namespace HVACrate.Data.Models
{
    public class Material
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Type { get; set; } = null!;

        public double ThermalConductivity { get; set; }

        public virtual ICollection<BuildingEnvelope> BuildingEnvelopes { get; set; } = [];
    }
}
