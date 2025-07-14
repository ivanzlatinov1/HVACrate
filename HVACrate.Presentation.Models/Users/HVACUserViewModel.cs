namespace HVACrate.Presentation.Models.ViewModels.Users
{
    public class HVACUserViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = null!;
    }
}
