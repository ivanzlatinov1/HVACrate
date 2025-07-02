using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HVACrate.Persistence.Configuration
{
    public class HVACUserConfiguration : IEntityTypeConfiguration<HVACUser>
    {
        public void Configure(EntityTypeBuilder<HVACUser> entity)
        {
            // Made primary key for HVACUser entity to be of type Guid
            entity
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
