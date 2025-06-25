using HVACrate.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HVACrate.Data
{
    public class HVACrateDbContext : IdentityDbContext<HVACUser, IdentityRole<Guid>, Guid>
    {
        // A constructor for debugging purposes
        public HVACrateDbContext()
        {
        }

        public HVACrateDbContext(DbContextOptions<HVACrateDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<BuildingEnvelope> BuildingsEnvelope { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
