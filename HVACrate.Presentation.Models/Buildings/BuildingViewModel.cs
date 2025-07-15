namespace HVACrate.Presentation.Models.Buildings
{
    public class BuildingViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string? ProjectName { get; set; }

        public Guid ProjectId { get; set; }

        public string? ImageUrl { get; set; }
    }
}