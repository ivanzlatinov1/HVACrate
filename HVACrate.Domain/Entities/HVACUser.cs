using Microsoft.AspNetCore.Identity;

namespace HVACrate.Domain.Entities
{
    public class HVACUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = [];
    }
}
