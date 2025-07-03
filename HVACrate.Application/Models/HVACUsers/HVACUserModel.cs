using HVACrate.Application.Models.Projects;

namespace HVACrate.Application.Models.HVACUsers
{
    public class HVACUserModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public DateTimeOffset RegisteredOn { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public ICollection<ProjectModel> Projects { get; set; } = [];
    }
}
