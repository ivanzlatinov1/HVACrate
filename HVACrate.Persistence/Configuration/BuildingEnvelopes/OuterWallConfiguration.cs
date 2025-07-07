using HVACrate.Domain.Entities.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.Domain.ValidationConstants.BuildingEnvelope;
using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Persistence.Configuration.BuildingEnvelopes
{
    public class OuterWallConfiguration : BuildingEnvelopeConfiguration, IEntityTypeConfiguration<OuterWall>
    {
        public void Configure(EntityTypeBuilder<OuterWall> entity)
        {
            entity
                .ToTable("OuterWalls");

            // Define constraints for the Direction column
            entity
                .Property(ow => ow.Direction)
                .IsRequired()
                .HasConversion<string>()
                .HasComment("The direction of the building envelope, e.g., North, East");

            // Define constraints for the AdjustedTemperature column
            entity
                .Property(ow => ow.AdjustedTemperature)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasDefaultValue(AdjustedTemperatureDefaultValue)
                .HasComment("Effective exterior temperature used in thermal transmission calculations (°C)");


            // Define constraints for the ShouldReduceHeatingArea column
            entity
                .Property(ow => ow.ShouldReduceHeatingArea)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("Indicates if heat transmission area should be reduced by window/door area on this wall");
        }
    }
}
