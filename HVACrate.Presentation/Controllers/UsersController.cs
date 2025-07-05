using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.FormModels;
using HVACrate.Presentation.Models.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants;
using static HVACrate.GCommon.GlobalConstants.QueryProperties;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> Index(BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<HVACUserModel> userModels = await this._userService.GetAllAsync(new()
            {
                SearchParam = query.SearchParam,
                QueryParam = UserQueryParam,
                Pagination = pagination
            }, cancellationToken);

            Dictionary<Guid, string> roles = await this._userService.GetRolesAsync([.. userModels.Items.Select(x => x.Id)], cancellationToken);

            HVACUserViewModel[] users = [.. userModels.Items.Select(x => new HVACUserViewModel
            {
                Id = x.Id,
                Username = x.Username,
                Role = roles[x.Id]
            })];

            return View((users, userModels.Count, pagination));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            HVACUserModel user = await this._userService.GetByIdAsync(id);

            HVACUserDetailsViewModel userViewModel = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                RegisteredOn = user.RegisteredOn.ToString(DateFormat),
                Role = await this._userService.GetRoleAsync(id),
                ProfilePictureUrl = user.ProfilePictureUrl ?? DefaultProfilePictureUrl,
                ProjectsCount = user.Projects.Count,
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this._userService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
