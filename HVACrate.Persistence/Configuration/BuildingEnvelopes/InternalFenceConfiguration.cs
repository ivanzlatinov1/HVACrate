using HVACrate.Domain.Entities.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HVACrate.Persistence.Configuration.BuildingEnvelopes
{
    public class InternalFenceConfiguration : BuildingEnvelopeConfiguration, IEntityTypeConfiguration<InternalFence>
    {
        public void Configure(EntityTypeBuilder<InternalFence> entity)
        {
            entity
                .ToTable("Internal fences");
        }
    }
}
