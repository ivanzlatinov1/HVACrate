using HVACrate.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using static HVACrate.Domain.ValidationConstants.ErrorMessages;
using static HVACrate.Domain.ValidationConstants.HVACUser;

namespace HVACrate.Presentation.Areas.Identity.Pages.Account
{
    public class RegisterModel(
        UserManager<HVACUser> userManager,
        SignInManager<HVACUser> signInManager) : PageModel
    {
        private readonly SignInManager<HVACUser> _signInManager = signInManager;
        private readonly UserManager<HVACUser> _userManager = userManager;

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

            [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
            [Display(Name = "Username")]
            public string? UserName { get; set; }

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
        }


        public async Task OnGetAsync(string returnUrl = null!)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null!)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = [.. (await _signInManager.GetExternalAuthenticationSchemesAsync())];
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.UserName = Input.UserName;
                user.Email = Input.Email;

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (!await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

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

        private HVACUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<HVACUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(HVACUser)}'. " +
                    $"Ensure that '{nameof(HVACUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
