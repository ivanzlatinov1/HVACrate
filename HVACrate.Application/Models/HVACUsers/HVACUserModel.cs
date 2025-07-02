using HVACrate.Application.Models.Projects;

namespace HVACrate.Application.Models.HVACUsers
{
    public class HVACUserModel
    {
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public ICollection<ProjectModel> Projects { get; set; } = [];
    }
}
