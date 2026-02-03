using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Extensions;
using HVACrate.Presentation.Models.Common;
using HVACrate.Presentation.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants;
using static HVACrate.GCommon.GlobalConstants.QueryProperties;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(IUserService userService,
        IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

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

            Dictionary<Guid, string> roles = await this._userService.GetRolesAsync(userModels.Items.Select(x => x.Id).ToArray(), cancellationToken);

            HVACUserViewModel[] users = userModels.Items.Select(x => new HVACUserViewModel
            {
                Id = x.Id,
                Username = x.Username,
                Role = roles[x.Id]
            }).ToArray();

            return View((users, userModels.Count, pagination));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken = default)
        {
            HVACUserModel user = await this._userService.GetByIdAsync(id, cancellationToken);

            HVACUserDetailsViewModel userViewModel = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                RegisteredOn = user.RegisteredOn.ToString(DateFormat),
                Role = await this._userService.GetRoleAsync(id, cancellationToken),
                ProfilePictureUrl = user.ProfilePictureUrl ?? DefaultProfilePictureUrl,
                ProjectsCount = user.Projects.Count,
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            HVACUserModel user = await this._userService.GetByIdAsync(id, cancellationToken);

            await this._userService.RemoveAsync(id, cancellationToken);
            if (user.ProfilePictureUrl is not null)
            {
                _webHostEnvironment.DeleteFile(user.ProfilePictureUrl);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
