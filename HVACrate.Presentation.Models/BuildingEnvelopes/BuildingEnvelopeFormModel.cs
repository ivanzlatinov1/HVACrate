using Microsoft.AspNetCore.Mvc.Rendering;

namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public abstract class BuildingEnvelopeFormModel
    {
        public Guid Id { get; set; }

        public abstract string Type { get; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Area { get; set; }

        public double Density { get; set; }

        public double AdjustedTemperature { get; set; }

        public double HeatTransferCoefficient { get; set; }

        public bool IsDeleted { get; set; }

        public Guid RoomId { get; set; }

        public Guid MaterialId { get; set; }

        public List<SelectListItem> Materials { get; set; } = [];
    }
}
