using HVACrate.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static HVACrate.Domain.ValidationConstants.HVACUser;
using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Presentation.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<HVACUser> _signInManager;
        private readonly UserManager<HVACUser> _userManager;

        public LoginModel(SignInManager<HVACUser> signInManager, UserManager<HVACUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Required]
        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public IList<AuthenticationScheme> ExternalLogins { get; set; } = null!;

        [BindProperty]
        public string ReturnUrl { get; set; } = null!;

        [TempData]
        public string ErrorMessage { get; set; } = null!;

        public class InputModel
        {
            [Required]
            [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
            [Display(Name = "Username")]
            public string Username { get; set; } = null!;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = null!;

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                HVACUser? user = await _userManager.FindByNameAsync(Input.Username);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "A user with these credentials does not exist.");
                    return Page();
                }

                await this._userManager.AddClaimAsync(user, new(ProfilePictureClaimType, user.ProfilePictureUrl ?? DefaultProfilePictureUrl));

                var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
