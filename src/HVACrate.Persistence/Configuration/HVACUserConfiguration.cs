using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static HVACrate.Domain.ValidationConstants.HVACUser;
using static HVACrate.GCommon.GlobalConstants;

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

            // Constraints for user's first name
            entity
                .Property(u => u.FirstName)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(FirstNameMaxLength);

            // Constraints for user's last name
            entity
                .Property(u => u.LastName)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(LastNameMaxLength);

            // Constraints for user's profile picture url
            entity
                .Property(u => u.ProfilePictureUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength);

            // Constraints for user's registration date
            entity
                .Property(u => u.RegisteredOn)
                .IsRequired();
        }
    }
}
