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
            // Define the table name and primary key for the Room
            entity
                .ToTable("Rooms")
                .HasKey(r => r.Id);

            // Define constraints for the Id column
            entity
                .Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Room's unique identifier");

            // Define constraints for the Type column
            entity
                .Property(r => r.Type)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(RoomTypeMaxLength)
                .HasComment("The type of the room, e.g., Bathroom, Bedroom");

            // Define constraints for the Number column
            entity
                .Property(r => r.Number)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(RoomNumberMaxLength)
                .HasComment("Number of the room, e.g., A101, B102");

            // Define constraints for the Temperature column
            entity
                .Property(r => r.Temperature)
                .IsRequired()
                .HasPrecision(5, 2)
                .HasComment("Internal room temperature");

            // Define constraints for the HeatLossTransmission column
            entity
                .Property(r => r.HeatLossTransmission)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasComment("Calculated heat loss from transmission");

            // Define constraints for the HeatLossInfiltration column
            entity
                .Property(r => r.HeatLossInfiltration)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasComment("Calculated heat loss from infiltration");

            // Define a query filter to exclude soft-deleted entities
            entity
                .HasQueryFilter(b => !b.IsDeleted);

            // Define constraints for the BuildingId column and its relationship with the Building entity
            entity
                .Property(r => r.BuildingId)
                .IsRequired()
                .HasComment("Reference to the building this room belongs to");

            entity
                .HasOne(r => r.Building)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define relationship with the BuildingEnvelopes entity
            entity
                .HasMany(r => r.BuildingEnvelopes)
                .WithOne(be => be.Room)
                .HasForeignKey(be => be.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
