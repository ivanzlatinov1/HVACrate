using System.ComponentModel;

namespace HVACrate.Presentation.Models.FormModels
{
    public class RoomFormModel
    {
        public Guid Id { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; } = null!;

        [DisplayName("Number")]
        public string Number { get; set; } = null!;

        [DisplayName("Room Temperature")]
        public double Temperature { get; set; }

        [DisplayName("The floor of which is the room")]
        public int Floor { get; set; }

        public int TotalFloors { get; set; }

        public Guid BuildingId { get; set; }
    }
}
