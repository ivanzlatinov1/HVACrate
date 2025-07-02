using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.GCommon.GlobalConstants;
using static HVACrate.Domain.ValidationConstants.Material;

namespace HVACrate.Persistence.Configuration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> entity)
        {
            // Define the table name and primary key for the Material
            entity
                .ToTable("Materials")
                .HasKey(m => m.Id);

            // Define constraints for the Id column
            entity
                .Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Material's unique identifier");

            // Define constraints for the Type column
            entity
                .Property(m => m.Type)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(TypeMaxLength)
                .HasComment("Material's type, e.g., Brick, Concrete");

            // Define constraints for the ThermalConductivity column
            entity
                .Property(m => m.ThermalConductivity)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Thermal conductivity in W/mK");

            // Define default value for IsDeleted property for soft deletion
            entity
                .Property(m => m.IsDeleted)
                .HasDefaultValue(false);

            // Define a query filter to exclude soft-deleted entities
            entity
                .HasQueryFilter(m => !m.IsDeleted);

            // Define constraints for the BuildingEnvelopeId column and its relationship with the BuildingEnvelope entity
            entity
                .HasMany(m => m.BuildingEnvelopes)
                .WithOne(be => be.Material)
                .HasForeignKey(be => be.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed initial data for the Material entity
            entity
                .HasData(SeedMaterials());
        }

        private static List<Material> SeedMaterials()
        {
            List<Material> materials =
            [
                new Material
                {
                    Id = Guid.Parse("3fdb3840-69b2-459b-8942-cb8fe226b3aa"),
                    Type = "Brick",
                    ThermalConductivity = 0.67
                },
                new Material
                {
                    Id = Guid.Parse("1f507fc6-87f7-4552-9b44-3f810c3c1af4"),
                    Type = "Copper",
                    ThermalConductivity = 380.0
                },
                new Material
                {
                    Id = Guid.Parse("fcbe3b1c-aeb6-440e-b1cb-eb19591072b8"),
                    Type = "Wood",
                    ThermalConductivity = 0.15
                },
                new Material
                {
                    Id = Guid.Parse("63e6a38b-4514-4242-93e2-c3793d71c163"),
                    Type = "Glass",
                    ThermalConductivity = 1.15
                },
                new Material
                {
                    Id = Guid.Parse("d0e38ecc-fc39-49c6-8425-44b2a68801e3"),
                    Type = "Limestone",
                    ThermalConductivity = 1.7
                },
                new Material
                {
                    Id = Guid.Parse("5dfb9e64-b1a2-4332-841b-c9ee60e5126d"),
                    Type = "Aluminium",
                    ThermalConductivity = 230.0
                },
                new Material
                {
                    Id = Guid.Parse("cd2fbf4d-3005-494a-a3b6-5acad97ff86a"),
                    Type = "Stone",
                    ThermalConductivity = 1.4
                },
                new Material
                {
                    Id = Guid.Parse("927bdf77-0ba2-4b7f-a265-9c586c1fd4a9"),
                    Type = "Rubber",
                    ThermalConductivity = 0.15
                }
            ];

            return materials;
        }
    }
}
