using HVACrate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HVACrate.Presentation.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel(
        UserManager<HVACUser> userManager) : PageModel
    {
        private readonly UserManager<HVACUser> _userManager = userManager;

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
