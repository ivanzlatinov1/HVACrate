using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.Buildings;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.Common;
using HVACrate.Presentation.Models.Rooms;
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken = default)
        {
            RoomModel room = await this._roomService.GetByIdAsync(id, cancellationToken);

            RoomDetailsViewModel details = new()
            {
                Id = room.Id,
                Type = room.Type,
                Number = room.Number,
                Floor = room.Floor,
                Temperature = room.Temperature,
                BuildingName = room.Building.Name,
                IsEnclosed = room.BuildingEnvelopes.Any(),
            };

            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken = default)
        {
            if (id == null) return NotFound();

            RoomModel building = await this._roomService.GetByIdAsync(id.Value, cancellationToken);

            RoomFormModel form = new()
            {
                Id = building.Id,
                Type = building.Type,
                Number = building.Number,
                Floor = building.Floor,
                Temperature = building.Temperature,
                BuildingId = building.BuildingId,
            };

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomFormModel form, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RoomModel model = await this._roomService.GetByIdAsync(form.Id, cancellationToken);
                    model.Type = form.Type;
                    model.Number = form.Number;
                    model.Floor = form.Floor;
                    model.Temperature = form.Temperature;

                    await this._roomService.UpdateAsync(model, cancellationToken);
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home");
                }
                return this.RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomModel? room = await this._roomService.GetByIdAsync(id.Value, cancellationToken);

            RoomViewModel roomViewModel = new()
            {
                Id = room.Id,
                Type = room.Type,
                Number = room.Number,
                Floor = room.Floor,
                BuildingId = room.BuildingId
            };

            return View(roomViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoomViewModel room, CancellationToken cancellationToken = default)
        {
            try
            {
                if (room != null)
                {
                    await this._roomService.SoftDeleteAsync(room.Id, cancellationToken);
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
