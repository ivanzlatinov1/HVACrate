using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Persistence.Configuration
{
    public class BuildingEnvelopeConfiguration : IEntityTypeConfiguration<BuildingEnvelope>
    {
        public void Configure(EntityTypeBuilder<BuildingEnvelope> entity)
        {
            // Define the table name and primary key for the BuildingEnvelope
            entity
                .ToTable("BuildingEnvelopes")
                .HasKey(be => be.Id);

            // Define constraints for the Id column
            entity
                .Property(be => be.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Building envelope's unique identifier");

            // Define constraints for the Type column
            entity
                .Property(be => be.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasComment("Type of the building envelope, e.g., OuterWall, Roof, Floor");

            // Define constraints for the Height column
            entity
                .Property(be => be.Height)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("The height of the building envelope");

            // Define constraints for the Width column
            entity
                .Property(be => be.Width)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("The width of the building envelope");

            // Define constraints for the Count column
            entity.Property(be => be.Count)
                .IsRequired()
                .HasComment("The count of the building envelopes");

            // Define constraints for the Density column
            entity
                .Property(be => be.Density)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("The density of the building envelope");

            // Define constraints for the ZOrientationCoefficient column
            entity
                .Property(be => be.ZOrientationCoefficient)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Orientation coefficient (Zo)");

            // Define default value for IsDeleted property for soft deletion
            entity
                .Property(be => be.IsDeleted)
                .HasDefaultValue(false);

            // Define a query filter to exclude soft-deleted entities
            entity
                .HasQueryFilter(be => !be.IsDeleted);

            // Define constraints for the RoomId column and its relationship with the Room entity
            entity
                .Property(be => be.RoomId)
                .IsRequired()
                .HasComment("Reference to the room this building envelope belongs to");

            entity
                .HasOne(be => be.Room)
                .WithMany(r => r.BuildingEnvelopes)
                .HasForeignKey(be => be.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define constraints for the MaterialId column and its relationship with the Material entity
            entity
                .Property(be => be.MaterialId)
                .IsRequired()
                .HasComment("Reference to the material of the building envelope");

            entity
                .HasOne(be => be.Material)
                .WithMany(m => m.BuildingEnvelopes)
                .HasForeignKey(be => be.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
