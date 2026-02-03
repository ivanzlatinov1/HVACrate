namespace HVACrate.Presentation.Models.Rooms
{
    public class RoomDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;

        public string Number { get; set; } = null!;

        public double Temperature { get; set; }

        public int Floor { get; set; }

        public Guid BuildingId { get; set; }

        public string BuildingName { get; set; } = null!;

        public bool IsEnclosed { get; set; }
    }
}
