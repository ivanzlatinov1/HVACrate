﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace HVACrate.Presentation.Models.BuildingEnvelopes
{
    public class OpeningFormModel : BuildingEnvelopeFormModel
    {
        public override string Type => "Windows And Doors";

        public string Direction { get; set; } = null!;

        public double JointLength { get; set; }

        public double VentilationCoefficient { get; set; }

        public int Count { get; set; }

        public List<SelectListItem> Directions { get; set; } = [];
    }
}
