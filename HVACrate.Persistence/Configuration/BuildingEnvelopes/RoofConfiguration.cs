using HVACrate.Domain.Entities.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.Domain.ValidationConstants.BuildingEnvelope;
using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Persistence.Configuration.BuildingEnvelopes
{
    internal class RoofConfiguration : BuildingEnvelopeConfiguration, IEntityTypeConfiguration<Roof>
    {
        public void Configure(EntityTypeBuilder<Roof> entity)
        {
            entity
                .ToTable("Roofs");

            // Define constraints for the AdjustedTemperature column
            entity
                .Property(r => r.AdjustedTemperature)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasDefaultValue(AdjustedTemperatureDefaultValue)
                .HasComment("Effective exterior temperature used in thermal transmission calculations (°C)");
        }
    }
}
