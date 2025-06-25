using HVACrate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.Data.Common.EntityConstants;

namespace HVACrate.Data.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        // Fluent API
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            entity
                .ToTable("Projects")
                .HasKey(p => p.Id);

            entity
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Project's unique identifier");

            entity
                .Property(p => p.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(NameMaxLength)
                .HasComment("Name of the project");

            entity
                .Property(p => p.RegionTemperature)
                .HasPrecision(18, 2)
                .HasComment("Average external temperature of the region");

            entity
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasComment("The date when the project was created");

            entity
                .HasMany(p => p.Buildings)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
