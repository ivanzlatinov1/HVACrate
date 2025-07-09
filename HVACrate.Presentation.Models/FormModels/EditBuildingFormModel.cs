using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace HVACrate.Presentation.Models.FormModels
{
    public class EditBuildingFormModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name of the building")]
        public string Name { get; set; } = null!;

        [DisplayName("Location")]
        public string Location { get; set; } = null!;

        [DisplayName("Total Height")]
        public double TotalHeight { get; set; }

        [DisplayName("Wind Speed")]
        public double WindSpeed { get; set; }

        [DisplayName("Number of floors")]
        public int Floors { get; set; }

        [DisplayName("Building's orientation")]
        public string Orientation { get; set; } = null!;

        public Guid ProjectId { get; set; }

        [DisplayName("A picture of the building")]
        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}
