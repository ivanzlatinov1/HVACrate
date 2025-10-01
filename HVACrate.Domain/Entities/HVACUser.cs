using Microsoft.AspNetCore.Identity;

namespace HVACrate.Domain.Entities
{
    public class HVACUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Username => this.UserName ?? string.Empty;

        public string? ProfilePictureUrl { get; set; }

        public DateTimeOffset RegisteredOn { get; set; } = DateTimeOffset.UtcNow;

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
