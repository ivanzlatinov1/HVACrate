using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.GCommon.GlobalConstants;
using static HVACrate.Domain.ValidationConstants.Building;

namespace HVACrate.Persistence.Configuration
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> entity)
        {
            // Define the table name and primary key for the Building entity
            entity
                .ToTable("Buildings")
                .HasKey(b => b.Id);

            // Define constraints for the Id column
            entity
                .Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Building's unique identifier");

            // Define constraints for Name column
            entity
                .Property(b => b.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(NameMaxLength)
                .HasComment("Name of the building");

            // Define constraints for Location column
            entity
                .Property(b => b.Location)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(LocationMaxLength)
                .HasComment("Building's geographical placement");

            // Define constraints for TotalHeight column
            entity
                .Property(b => b.TotalHeight)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Total height of the building");

            // Define constraints for WindSpeed column
            entity
                .Property(b => b.WindSpeed)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Wind speed used for infiltration");

            // Define constraints for ImageUrl column
            entity
                .Property(b => b.ImageUrl)
                .IsUnicode()
                .HasMaxLength(ImageUrlMaxLength)
                .HasComment("An image of the building");

            // Define constraints for ProjectId column
            entity
                .Property(b => b.ProjectId)
                .IsRequired()
                .HasComment("Reference to the project this building belongs to");

            // Define default value for IsDeleted property for soft deletion
            entity
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            // Adding query filter to exclude soft-deleted entities
            entity
                .HasQueryFilter(b => !b.IsDeleted);

            // Define relationships with other entities
            entity
                .HasOne(b => b.Project)
                .WithMany(p => p.Buildings)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasMany(b => b.Rooms)
                .WithOne(r => r.Building)
                .HasForeignKey(r => r.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
