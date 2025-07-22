using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static HVACrate.Domain.ValidationConstants.Room;
using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Persistence.Configuration
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
                .HasMaxLength(TypeMaxLength)
                .HasComment("The type of the room, e.g., Bathroom, Bedroom");

            // Define constraints for the Number column
            entity
                .Property(r => r.Number)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(NumberMaxLength)
                .HasComment("Number of the room, e.g., A101, B102");

            // Define constraints for the Floor column
            entity
                .Property(r => r.Floor)
                .IsRequired()
                .HasComment("The room's floor");

            // Define constraints for the Temperature column
            entity
                .Property(r => r.Temperature)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Internal room temperature");

            // Define default value for IsDeleted property for soft deletion
            entity
                .Property(r => r.IsDeleted)
                .HasDefaultValue(false);

            // Define a query filter to exclude soft-deleted entities
            entity
                .HasQueryFilter(r => !r.IsDeleted);

            // Define constraints for the BuildingId column and its relationship with the Building entity
            entity
                .Property(r => r.BuildingId)
                .IsRequired()
                .HasComment("Reference to the building this room belongs to");

            entity
                .HasOne(r => r.Building)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BuildingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define relationship with the BuildingEnvelopes entity
            entity
                .HasMany(r => r.BuildingEnvelopes)
                .WithOne(be => be.Room)
                .HasForeignKey(be => be.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
