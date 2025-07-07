namespace HVACrate.Presentation.Models.ViewModels.Buildings
{
    public class BuildingViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}