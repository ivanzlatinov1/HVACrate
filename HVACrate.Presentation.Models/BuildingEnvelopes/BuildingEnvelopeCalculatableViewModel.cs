namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public class BuildingEnvelopeCalculatableViewModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public string? Direction { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Area { get; set; }

        public double HeatTransferCoefficient { get; set; }

        public double AdjustedTemperature { get; set; }

        public int Count { get; set; }

        public string RoomName { get; set; } = null!;

        public double RoomTemperature { get; set; }

        public double HeatLoss { get; set; } = 0.0;
    }
}
