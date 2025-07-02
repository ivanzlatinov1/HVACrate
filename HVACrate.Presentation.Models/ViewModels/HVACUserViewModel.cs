namespace HVACrate.Presentation.Models.ViewModels
{
    public class HVACUserViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string Role { get; set; } = null!;
    }
}
