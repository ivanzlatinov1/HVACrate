using HVACrate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.Data.Common.EntityConstants;

namespace HVACrate.Data.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> entity)
        {
            entity
                .ToTable("Rooms")
                .HasKey(r => r.Id);

            entity
                .Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Room's unique identifier");

            entity
                .Property(r => r.Type)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(RoomTypeMaxLength)
                .HasComment("The type of the room, e.g., Bathroom, Bedroom");

            entity
                .Property(r => r.Number)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(RoomNumberMaxLength)
                .HasComment("Number of the room, e.g., A101, B102");

            entity
                .Property(r => r.Temperature)
                .IsRequired()
                .HasPrecision(5, 2)
                .HasComment("Internal room temperature");

            entity
                .Property(r => r.HeatLossTransmission)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasComment("Calculated heat loss from transmission");

            entity
                .Property(r => r.HeatLossInfiltration)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasComment("Calculated heat loss from infiltration");

            entity
                .Property(r => r.BuildingId)
                .IsRequired()
                .HasComment("Reference to the building this room belongs to");

            entity
                .HasOne(r => r.Building)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasMany(r => r.BuildingEnvelopes)
                .WithOne(be => be.Room)
                .HasForeignKey(be => be.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
