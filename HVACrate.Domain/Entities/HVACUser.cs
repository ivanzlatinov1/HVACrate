using Microsoft.AspNetCore.Identity;

namespace HVACrate.Domain.Entities
{
    public class HVACUser : IdentityUser<Guid>
    {
        public virtual ICollection<Project> Projects { get; set; } = [];
    }
}
