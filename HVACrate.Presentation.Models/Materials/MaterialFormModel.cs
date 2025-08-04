namespace HVACrate.Presentation.Models.Materials
{
    public class MaterialFormModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public double ThermalConductivity { get; set; }

        public Guid? BuildingEnvelopeRoomId { get; set; }

        public string? BuildingEnvelopeType { get; set; }
    }
}
