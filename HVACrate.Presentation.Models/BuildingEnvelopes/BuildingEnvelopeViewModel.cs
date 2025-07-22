namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public class BuildingEnvelopeViewModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public int Count { get; set; }
    }
}
