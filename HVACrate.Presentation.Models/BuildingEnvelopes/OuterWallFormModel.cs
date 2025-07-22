using Microsoft.AspNetCore.Mvc.Rendering;

namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public class OuterWallFormModel : BuildingEnvelopeFormModel
    {
        public override string Type => "Outer Wall";

        public string Direction { get; set; } = null!;

        public bool ShouldReduceHeatingArea { get; set; }

        public List<SelectListItem> Directions { get; set; } = [];
    }
}
