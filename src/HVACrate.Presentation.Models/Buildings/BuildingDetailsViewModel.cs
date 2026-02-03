namespace HVACrate.Presentation.Models.Buildings
{
    public class BuildingDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public double TotalHeight { get; set; }

        public double WindSpeed { get; set; }

        public int Floors { get; set; }

        public string Orientation { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;
    }
}
