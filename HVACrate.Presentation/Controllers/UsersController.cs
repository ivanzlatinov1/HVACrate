using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.FormModels;
using HVACrate.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> Index(HVACUserQueryFormModel query, CancellationToken cancellationToken = default)
        {
            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<HVACUserModel> userModels = await this._userService.GetAllAsync(new()
            {
                SearchParam = query.SearchParam,
                Sorting = new(Type: query.SortingType, Direction: query.SortingDirection),
                Pagination = pagination
            }, cancellationToken);

            Dictionary<Guid, string> roles = await this._userService.GetRolesAsync([.. userModels.Items.Select(x => x.Id)], cancellationToken);

            HVACUserViewModel[] users = [.. userModels.Items.Select(x => new HVACUserViewModel
            {
                Id = x.Id.ToString(),
                Username = x.Username,
                Email = x.Email,
                RegisteredOn = x.RegisteredOn.ToString(DateFormat),
                Role = roles[x.Id]
            })];

            return View((users, userModels.Count, pagination));
        }
    }
}
