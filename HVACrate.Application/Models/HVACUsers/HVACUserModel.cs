using HVACrate.Application.Models.Projects;
using System.ComponentModel.DataAnnotations;
using static HVACrate.Domain.ValidationConstants.HVACUser;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;

namespace HVACrate.Application.Models.HVACUsers
{
    public class HVACUserModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = Required)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength, ErrorMessage = StringLength)]
        public string Username { get; set; } = string.Empty;

        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = StringLength)]
        public string? FirstName { get; set; }

        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength, ErrorMessage = StringLength)]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string? Email { get; set; }

        public DateTimeOffset RegisteredOn { get; set; }

        [Url(ErrorMessage = InvalidUrl)]
        public string? ProfilePictureUrl { get; set; }

        public ICollection<ProjectModel> Projects { get; set; } = [];
    }
}
