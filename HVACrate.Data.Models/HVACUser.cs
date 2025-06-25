using Microsoft.AspNetCore.Identity;

namespace HVACrate.Data.Models
{
    public class HVACUser : IdentityUser<Guid>
    {
        public HVACUser()
        {
            this.Id = Guid.NewGuid();
        }

        public virtual ICollection<Project> Projects { get; set; } = [];
    }
}
