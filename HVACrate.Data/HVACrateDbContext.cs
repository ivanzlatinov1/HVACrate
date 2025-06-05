using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Data
{
    public class HVACrateDbContext : IdentityDbContext
    {
        // A constructor for debugging purposes
        public HVACrateDbContext()
        {
        }

        public HVACrateDbContext(DbContextOptions<HVACrateDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // TODO: Add models' configurations here.

            base.OnModelCreating(builder);
        }
    }
}
