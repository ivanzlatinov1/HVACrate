using HVACrate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HVACrate.Data.Configuration
{
    public class HVACUserConfiguration : IEntityTypeConfiguration<HVACUser>
    {
        public void Configure(EntityTypeBuilder<HVACUser> entity)
        {
            entity
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
