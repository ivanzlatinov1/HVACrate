using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Application.Models.Buildings;

namespace HVACrate.Application.Models.Rooms
{
    public class RoomModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;

        public string Number { get; set; } = null!;

        public double Temperature { get; set; }

        public int Floor { get; set; }

        public bool IsEnclosed { get; set; }

        public bool IsDeleted { get; set; }

        public Guid BuildingId { get; set; }

        public virtual BuildingModel Building { get; set; } = null!;

        public virtual ICollection<BuildingEnvelopeModel> BuildingEnvelopes { get; set; } = [];
    }
}
