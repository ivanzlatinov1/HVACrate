using HVACrate.Domain.Entities;
using HVACrate.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static HVACrate.Domain.ValidationConstants.HVACUser;
using static HVACrate.Domain.ValidationConstants.ValidationMessages;
using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Presentation.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<HVACUser> _signInManager;
        private readonly UserManager<HVACUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public RegisterModel(
        UserManager<HVACUser> userManager,
        SignInManager<HVACUser> signInManager,
        IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public string ReturnUrl { get; set; } = null!;

        public IList<AuthenticationScheme> ExternalLogins { get; set; } = null!;

        public class InputModel
        {
            [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
            [Display(Name = "First Name")]
            public string? FirstName { get; set; }

            [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
            [Display(Name = "Last Name")]
            public string? LastName { get; set; }

            [Required]
            [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
            [Display(Name = "Username")]
            public string Username { get; set; } = null!;

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = null!;

            [Required]
            [StringLength(100, ErrorMessage = StringLength, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = null!;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = null!;

            [Display(Name = "Profile Picture")]
            public IFormFile? ProfilePicture { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null!)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null!)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                HVACUser user = new()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Username,
                    Email = Input.Email,
                    ProfilePictureUrl = Input.ProfilePicture == null
                        ? null
                        : await _env.UploadImageAsync(Input.ProfilePicture, Input.Username)
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (!await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    await this._userManager.AddClaimAsync(user, new(ProfilePictureClaimType, user.ProfilePictureUrl ?? DefaultProfilePictureUrl));

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
