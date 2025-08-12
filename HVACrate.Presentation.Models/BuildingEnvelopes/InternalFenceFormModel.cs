namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public class InternalFenceFormModel : BuildingEnvelopeFormModel
    {
        public override string Type => "Internal Fence";

        public int Count { get; set; }

        public int AdjacentRoomTemperature { get; set; }
    }
}
