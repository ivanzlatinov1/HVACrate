using HVACrate.Domain.Entities.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HVACrate.Persistence.Configuration.BuildingEnvelopes
{
    public class InternalFenceConfiguration : BuildingEnvelopeConfiguration, IEntityTypeConfiguration<InternalFence>
    {
        public void Configure(EntityTypeBuilder<InternalFence> entity)
        {
            // Define constraints for the Count column
            entity
                .Property(be => be.Count)
                .IsRequired()
                .HasComment("The count of the internal fences");

            // Define constraints for the AdjacentRoomTemperature column
            entity
                .Property(be => be.AdjacentRoomTemperature)
                .IsRequired()
                .HasComment("The room temperature of the neighboor room");
        }
    }
}
