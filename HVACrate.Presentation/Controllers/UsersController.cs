using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.HVACUsers;
using HVACrate.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        public async Task<IActionResult> Index(HVACUserQueryModel query, CancellationToken cancellationToken = default)
        {
            ICollection<HVACUserModel> users = await this._userService.GetAllAsync(query, cancellationToken);

            Dictionary<Guid, string> roles = await this._userService.GetRolesAsync([.. users.Select(x => x.Id)], cancellationToken);

            HVACUserViewModel[] hVACUsers = [.. users.Select(x => new HVACUserViewModel
            {
                Id = x.Id.ToString(),
                UserName = x.UserName,
                Email = x.Email,
                Role = roles[x.Id]
            })];

            return View(hVACUsers);
        }
    }
}
