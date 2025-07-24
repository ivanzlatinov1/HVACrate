namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public class BuildingEnvelopeDetailViewModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public double Height { get; set; }

        public double Width { get; set; }

        public double Area { get; set; }

        public double Density { get; set; }

        public double HeatTransferCoefficient { get; set; }

        public double AdjustedTemperature { get; set; }

        public string RoomName { get; set; } = null!;

        public string Material { get; set; } = null!;
    }
}
