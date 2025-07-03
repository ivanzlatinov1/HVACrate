namespace HVACrate.Presentation.Models.ViewModels
{
    public class HVACUserViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string RegisteredOn { get; set; } = string.Empty;

        public string Role { get; set; } = null!;
    }
}
