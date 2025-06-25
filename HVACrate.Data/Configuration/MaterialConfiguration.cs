using HVACrate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HVACrate.Data.Configuration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> entity)
        {
            entity
                .ToTable("Materials")
                .HasKey(m => m.Id);

            entity
                .Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Material's unique identifier");

            entity
                .Property(m => m.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasComment("Material's type, e.g., Brick, Concrete");

            entity
                .Property(m => m.ThermalConductivity)
                .IsRequired()
                .HasComment("Thermal conductivity in W/mK");

            entity
                .HasMany(m => m.BuildingEnvelopes)
                .WithOne(be => be.Material)
                .HasForeignKey(be => be.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
