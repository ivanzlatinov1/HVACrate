namespace HVACrate.Presentation.Models.Rooms
{
    public class RoomCalculatableViewModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public string Number { get; set; } = null!;

        public string BuildingName { get; set; } = null!;

        public Guid ProjectId { get; set; }
    }
}
