using HVACrate.Domain.Entities.BuildingEnvelopes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Persistence.Configuration.BuildingEnvelopes
{
    public class OpeningConfiguration : BuildingEnvelopeConfiguration, IEntityTypeConfiguration<Opening>
    {
        public void Configure(EntityTypeBuilder<Opening> entity)
        {
            // Define constraints for the Direction column
            entity
                .Property(o => o.Direction)
                .IsRequired()
                .HasConversion<string>()
                .HasComment("The direction of the building envelope, e.g., North, East");

            // Define constraints for the JointLength column
            entity
                .Property(o => o.JointLength)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Length of joints surrounding the opening, used in heat loss calculation (m)");

            // Define constraints for the Count column
            entity
                .Property(be => be.Count)
                .IsRequired()
                .HasComment("The count of the openings");

            // Define constraints for the VentilationCoefficient column
            entity
                .Property(o => o.VentilationCoefficient)
                .IsRequired()
                .HasPrecision(TotalPrecision, TotalScale)
                .HasComment("Air exchange coefficient for ventilation through the opening");
        }
    }
}
