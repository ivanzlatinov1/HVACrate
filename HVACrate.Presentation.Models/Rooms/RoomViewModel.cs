namespace HVACrate.Presentation.Models.ViewModels.Rooms
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;

        public string? Number { get; set; }

        public int Floor { get; set; }

        public Guid BuildingId { get; set; }
    }
}
