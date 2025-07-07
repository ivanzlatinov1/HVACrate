using HVACrate.Domain.Entities.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Persistence.Configuration.BuildingEnvelopes
{
    public class FloorConfiguration : BuildingEnvelopeConfiguration, IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> entity)
        {
            entity
                .ToTable("Floors");

            // Define constraints for the ThermalConductivityResistance column
            entity
                .Property(f => f.ThermalConductivityResistance)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Resistance to heat flow through the floor structure (W·m²/K)");

            // Define constraints for the GroundWaterLength column
            entity
                .Property(f => f.GroundWaterLength)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Length of the floor's contact with groundwater (m)");

            // Define constraints for the GroundWaterTemperature column
            entity
                .Property(f => f.GroundWaterTemperature)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Temperature of groundwater under the floor (°C)");
        }
    }
}
