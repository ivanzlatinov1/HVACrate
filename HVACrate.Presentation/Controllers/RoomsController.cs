using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
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
        public async Task<IActionResult> Index(Guid id, BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return NotFound();

            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<RoomModel> roomModels = await this._roomService.GetAllAsReadOnlyAsync(new()
            {
                SearchParam = query.SearchParam,
                QueryParam = RoomQueryParam,
                Pagination = pagination
            }, id, cancellationToken);

            RoomViewModel[] rooms = [.. roomModels.Items.Select(x => x.ToView())];

            int totalFloors = await this._buildingService.GetTotalFloors(id, cancellationToken);

            ViewBag.SelectedFloor = query.SearchParam;

            return View((id, totalFloors, rooms, roomModels.Count, pagination));
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

                RoomModel model = form.ToModel();

                await _roomService.CreateAsync(model, cancellationToken);

                return this.RedirectToAction(nameof(Index), new { id = form.BuildingId });
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

            RoomDetailsViewModel details = room.ToDetailsViewModel();

            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken = default)
        {
            if (id == null) return NotFound();

            RoomModel building = await this._roomService.GetByIdAsync(id.Value, cancellationToken);

            RoomFormModel form = building.ToForm();

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
                    model.UpdateFromForm(form);

                    await this._roomService.UpdateAsync(model, cancellationToken);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { ErrorMessage = ex.Message });
                }
                return this.RedirectToAction(nameof(Index), new { id = form.BuildingId });
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

            RoomViewModel roomViewModel = room.ToView();

            return View(roomViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoomViewModel room, CancellationToken cancellationToken = default)
        {
            try
            {
                await this._roomService.SoftDeleteAsync(room.Id, cancellationToken);
                return this.RedirectToAction(nameof(Index), new { id = room.BuildingId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = ex.Message });
            }
        }
    }
}
