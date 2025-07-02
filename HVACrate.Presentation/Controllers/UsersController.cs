using HVACrate.Application.Interfaces;
using HVACrate.Application.Models;
using HVACrate.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        public async Task<IActionResult> Index(string? search, CancellationToken cancellationToken)
        {
            ICollection<HVACUserModel>? users;

            if (search != null)
            {
                users = await this._userService.FindByUsernameAsync(search, cancellationToken) ?? [];
            }
            else
            {
                users = await this._userService.GetAllAsync(cancellationToken) ?? [];
            }

            HVACUserViewModel[] hVACUsers = await Task.WhenAll(users.Select(async x => new HVACUserViewModel
            {
                Id = x.Id.ToString(),
                UserName = x.UserName,
                Email = x.Email,
                IsAdmin = await this._userService.IsAdmin(x)
            }));

            return View(hVACUsers);
        }
    }
}
