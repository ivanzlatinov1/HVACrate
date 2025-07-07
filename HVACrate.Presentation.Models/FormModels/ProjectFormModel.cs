using System.ComponentModel;

namespace HVACrate.Presentation.Models.FormModels
{
    public class ProjectFormModel
    {
        public Guid Id { get; set; }

        [DisplayName("Project Name")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Region Temperature")]
        public double RegionTemperature { get; set; }
    }
}
