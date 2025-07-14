using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.Buildings;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.FormModels;
using HVACrate.Presentation.Models.ViewModels.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants.QueryProperties;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class RoomsController(IRoomService roomService, IBuildingService buildingService) : Controller
    {
        private readonly IRoomService _roomService = roomService;
        private readonly IBuildingService _buildingService = buildingService;

        [HttpGet]
        public async Task<IActionResult> Index(BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            Guid buildingId = Guid.Parse(Convert.ToString(TempData["BuildingId"]) ?? string.Empty);
            if (buildingId == Guid.Empty) return NotFound();

            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<RoomModel> roomModels = await this._roomService.GetAllAsReadOnlyAsync(new()
            {
                SearchParam = query.SearchParam,
                QueryParam = RoomQueryParam,
                Pagination = pagination
            }, buildingId, cancellationToken);

            BuildingModel building = await this._buildingService.GetByIdAsReadOnlyAsync(buildingId, cancellationToken);
            int totalFloors = building.Floors;

            RoomViewModel[] rooms = [.. roomModels.Items.Select(x => new RoomViewModel
            {
                Id = x.Id,
                Type = x.Type,
                Number = x.Number,
                Floor = x.Floor,
                BuildingId = buildingId,
            })];

            ViewBag.SelectedFloor = query.SearchParam;

            return View((buildingId, totalFloors, rooms, roomModels.Count, pagination));
        }
         [HttpGet]
        public IActionResult Create(Guid buildingId, int totalFloors)
        {
            RoomFormModel form = new()
            {
                BuildingId = buildingId,
                TotalFloors = totalFloors
            };
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomFormModel form, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);

                RoomModel model = new()
                {
                    Type = form.Type,
                    Number = form.Number,
                    Temperature = form.Temperature,
                    Floor = form.Floor,
                    BuildingId = form.BuildingId,
                };

                await _roomService.CreateAsync(model, cancellationToken);

                TempData["BuildingId"] = form.BuildingId;
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(form);
            }
        }
    }
}
