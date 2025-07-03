namespace HVACrate.Presentation.Models.ViewModels
{
    public class HVACUserDetailsViewModel
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Username { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string RegisteredOn { get; set; } = string.Empty;

        public string Role { get; set; } = null!;

        public string ProfilePictureUrl { get; set; } = null!;

        public int ProjectsCount { get; set; }
    }
}
