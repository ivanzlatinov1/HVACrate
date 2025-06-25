using HVACrate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.Data.Common.EntityConstants;

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
                .IsUnicode()
                .HasMaxLength(MaterialTypeMaxLength)
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

            entity
                .HasData(SeedMaterials());
        }

        private static List<Material> SeedMaterials()
        {
            List<Material> materials =
            [
                new Material
                {
                    Type = "Brick",
                    ThermalConductivity = 0.67
                },
                new Material
                {
                    Type = "Copper",
                    ThermalConductivity = 380.0
                },
                new Material
                {
                    Type = "Wood",
                    ThermalConductivity = 0.15
                },
                new Material
                {
                    Type = "Glass",
                    ThermalConductivity = 1.15
                },
                new Material
                {
                    Type = "Limestone",
                    ThermalConductivity = 1.7
                },
                new Material
                {
                    Type = "Aluminium",
                    ThermalConductivity = 230.0
                },
                new Material
                {
                    Type = "Stone",
                    ThermalConductivity = 1.4
                },
                new Material
                {
                    Type = "Rubber",
                    ThermalConductivity = 0.15
                }
            ];

            return materials;
        }
    }
}
