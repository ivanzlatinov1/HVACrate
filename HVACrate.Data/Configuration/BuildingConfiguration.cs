using HVACrate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.Data.Common.EntityConstants;

namespace HVACrate.Data.Configuration
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> entity)
        {
            entity
                .ToTable("Buildings")
                .HasKey(b => b.Id);

            entity
                .Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Building's unique identifier");

            entity
                .Property(b => b.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(NameMaxLength)
                .HasComment("Name of the building");

            entity
                .Property(b => b.Location)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(LocationMaxLength)
                .HasComment("Building's geographical placement");

            entity
                .Property(b => b.TotalHeight)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Total height of the building");

            entity
                .Property(b => b.WindSpeed)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Wind speed used for infiltration");

            entity
                .Property(b => b.ImageUrl)
                .IsUnicode()
                .HasMaxLength(ImageUrlMaxLength)
                .HasComment("An image of the building");

            entity
                .Property(b => b.ProjectId)
                .IsRequired()
                .HasComment("Reference to the project this building belongs to");

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
