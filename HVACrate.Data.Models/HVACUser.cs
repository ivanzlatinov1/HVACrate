using Microsoft.AspNetCore.Identity;

namespace HVACrate.Data.Models
{
    public class HVACUser : IdentityUser<Guid>
    {
        public virtual ICollection<Project> Projects { get; set; } = [];
    }
}
