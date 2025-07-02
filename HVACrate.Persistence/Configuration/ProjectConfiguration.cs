using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.GCommon.GlobalConstants;
using static HVACrate.Domain.ValidationConstants.Project;

namespace HVACrate.Persistence.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            // Define the table name and primary key for the Project entity
            entity
                .ToTable("Projects")
                .HasKey(p => p.Id);

            // Define constraints for the Id column
            entity
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Project's unique identifier");

            // Define constraints for the Name column
            entity
                .Property(p => p.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(NameMaxLength)
                .HasComment("Name of the project");

            // Define constraints for the Description column
            entity
                .Property(p => p.RegionTemperature)
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Average external temperature of the region");

            // Define constraints for the RegionTemperature column
            entity
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasComment("The date when the project was created");

            // Define default value for IsDeleted property for soft deletion
            entity
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            // Define a query filter to exclude soft-deleted entities
            entity
                .HasQueryFilter(p => !p.IsDeleted);

            // Define relationship constraints for the Project entity
            entity
                .HasMany(p => p.Buildings)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
