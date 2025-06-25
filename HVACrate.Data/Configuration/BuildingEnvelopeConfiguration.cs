using HVACrate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HVACrate.Data.Configuration
{
    public class BuildingEnvelopeConfiguration : IEntityTypeConfiguration<BuildingEnvelope>
    {
        public void Configure(EntityTypeBuilder<BuildingEnvelope> entity)
        {
            entity
                .ToTable("BuildingEnvelopes")
                .HasKey(be => be.Id);

            entity
                .Property(be => be.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Building envelope's unique identifier");

            entity
                .Property(be => be.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasComment("Type of the building envelope, e.g., Wall, Roof, Floor");

            entity
                .Property(be => be.Direction)
                .IsRequired()
                .HasConversion<string>()
                .HasComment("The direction of the building envelope, e.g., North, East");

            entity
                .Property(be => be.ZOrientationCoefficient)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComment("Orientation coefficient (Zo)");

            entity
                .Property(be => be.RoomId)
                .IsRequired()
                .HasComment("Reference to the room this building envelope belongs to");

            entity
                .HasOne(be => be.Room)
                .WithMany(r => r.BuildingEnvelopes)
                .HasForeignKey(be => be.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

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
