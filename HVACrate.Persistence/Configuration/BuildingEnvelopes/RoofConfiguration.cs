using HVACrate.Domain.Entities.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HVACrate.Persistence.Configuration.BuildingEnvelopes
{
    internal class RoofConfiguration : BuildingEnvelopeConfiguration, IEntityTypeConfiguration<Roof>
    {
        public void Configure(EntityTypeBuilder<Roof> entity)
        {
            entity
                .ToTable("Roofs");
        }
    }
}
