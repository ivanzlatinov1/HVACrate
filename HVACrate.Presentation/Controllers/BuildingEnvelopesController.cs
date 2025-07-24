using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.BuildingEnvelopes;
using HVACrate.Presentation.Models.Buildings;
using HVACrate.Presentation.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class BuildingEnvelopesController(IBuildingEnvelopeService buildingEnvelopeService,
        IRoomService roomService, IMaterialService materialService) : Controller
    {
        private readonly IBuildingEnvelopeService _buildingEnvelopeService = buildingEnvelopeService;
        private readonly IRoomService _roomService = roomService;
        private readonly IMaterialService _materialService = materialService;

        [HttpGet]
        public async Task<IActionResult> Index(Guid id, BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return NotFound();

            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            var buildingEnvelopeModels = await this._buildingEnvelopeService.GetAllAsReadOnlyAsync(id, cancellationToken);

            BuildingEnvelopeViewModel[] buildingEnvelopes = [.. buildingEnvelopeModels.Select(x => x.ToView())];

            long internalFencesCount = await this._buildingEnvelopeService.GetInternalFencesCountByRoom(id, cancellationToken);
            long openingsCount = await this._buildingEnvelopeService.GetOpeningsCountByRoom(id, cancellationToken);
            string roomNumber = await this._roomService.GetRoomNumberAsync(id, cancellationToken);
            Guid buildingId = await this._roomService.GetBuildingIdAsync(id, cancellationToken);

            bool isRoofAlreadyCreated = await this._buildingEnvelopeService.IsThereARoofInRoomAsync(id, cancellationToken);
            bool isFloorAlreadyCreated = await this._buildingEnvelopeService.IsThereAFloorInRoomAsync(id, cancellationToken);

            return View((id, buildingId, roomNumber, buildingEnvelopes, internalFencesCount,
                openingsCount, isRoofAlreadyCreated, isFloorAlreadyCreated, pagination));
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid roomId, string type, CancellationToken cancellationToken = default)
        {
            var materials = await InitializeMaterials(cancellationToken);
            var directions = InitializeDirections();

            switch (type)
            {
                case "OuterWall":
                    var outerWallForm = new OuterWallFormModel { RoomId = roomId, Directions = directions, Materials = materials };
                    return View(nameof(CreateOuterWall), outerWallForm);

                case "Opening":
                    var openingForm = new OpeningFormModel { RoomId = roomId, Directions = directions, Materials = materials };
                    return View(nameof(CreateOpening), openingForm);

                case "Floor":
                    var floorForm = new FloorFormModel { RoomId = roomId, Materials = materials };
                    return View(nameof(CreateFloor), floorForm);

                case "InternalFence":
                    var internalFenceForm = new InternalFenceFormModel { RoomId = roomId, Materials = materials };
                    return View(nameof(CreateInternalFence), internalFenceForm);

                case "Roof":
                    var roofForm = new RoofFormModel { RoomId = roomId, Materials = materials };
                    return View(nameof(CreateRoof), roofForm);

                default:
                    return this.RedirectToAction("Error", "Home", new { ErrorMessage = $"Type {type} does not exist in current context." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOuterWall(OuterWallFormModel form, CancellationToken cancellationToken = default)
        {
            Direction direction = Enum.Parse<Direction>(form.Direction);
            bool isWallAlreadyCreated = await this._buildingEnvelopeService.IsThereAWallOnDirectionAsync(form.RoomId, direction, cancellationToken);

            if (isWallAlreadyCreated)
            {
                ModelState.AddModelError(string.Empty, "There is already a wall in that direction!");

                await SeedFormDropdownsAsync(form, cancellationToken);
                return View(nameof(CreateOuterWall), form);
            }

            form.Area = form.Width * form.Height;
            form.AdjustedTemperature = this._buildingEnvelopeService.CalculateTemperatureCoefficient(form.Density, form.Type);

            return await HandleCreateAsync(form, f => f.ToModelFromForm(), cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOpening(OpeningFormModel form, CancellationToken cancellationToken = default)
        {
            Direction direction = Enum.Parse<Direction>(form.Direction);
            OuterWallModel? outerWall = await this._buildingEnvelopeService.GetWallByDirectionAsync(form.RoomId, direction, cancellationToken);

            if (outerWall is null)
            {
                ModelState.AddModelError(string.Empty, "There is no wall on that direction for putting a window/door on it!");

                await SeedFormDropdownsAsync(form, cancellationToken);
                return View(nameof(CreateOpening), form);
            }
            else if (!outerWall.ShouldReduceHeatingArea)
            {
                ModelState.AddModelError(string.Empty, $"On the wall with direction: {direction} cannot be reduced heating area!");

                await SeedFormDropdownsAsync(form, cancellationToken);
                return View(nameof(CreateOpening), form);
            }
            else if (outerWall.Area - (form.Width * form.Height * form.Count) < 0)
            {
                ModelState.AddModelError(string.Empty, $"The window/door is too big to suit the wall!");

                await SeedFormDropdownsAsync(form, cancellationToken);
                return View(nameof(CreateOpening), form);
            }

            outerWall.Area -= form.Width * form.Height * form.Count;
            await this._buildingEnvelopeService.UpdateAsync(outerWall, cancellationToken);

            form.Area = form.Width * form.Height;
            form.AdjustedTemperature = this._buildingEnvelopeService.CalculateTemperatureCoefficient(form.Density, form.Type);

            return await HandleCreateAsync(form, f => f.ToModelFromForm(), cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFloor(FloorFormModel form, CancellationToken cancellationToken = default)
        {
            bool isFloorAlreadyCreated = await this._buildingEnvelopeService.IsThereAFloorInRoomAsync(form.RoomId, cancellationToken);

            if (isFloorAlreadyCreated)
            {
                ModelState.AddModelError(string.Empty, "There is already a floor in this room!");

                await SeedFormDropdownsAsync(form, cancellationToken);
                return View(nameof(CreateFloor), form);
            }

            form.Area = form.Width * form.Height;
            form.AdjustedTemperature = this._buildingEnvelopeService.CalculateTemperatureCoefficient(form.Density, form.Type);

            return await HandleCreateAsync(form, f => f.ToModelFromForm(), cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInternalFence(InternalFenceFormModel form, CancellationToken cancellationToken = default)
        {
            form.Area = form.Width * form.Height;
            form.AdjustedTemperature = this._buildingEnvelopeService.CalculateTemperatureCoefficient(form.Density, form.Type);

            return await HandleCreateAsync(form, f => f.ToModelFromForm(), cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoof(RoofFormModel form, CancellationToken cancellationToken = default)
        {
            bool isRoofAlreadyCreated = await this._buildingEnvelopeService.IsThereARoofInRoomAsync(form.RoomId, cancellationToken);
            if (isRoofAlreadyCreated)
            {
                ModelState.AddModelError(string.Empty, "There is already a roof in this room!");

                await SeedFormDropdownsAsync(form, cancellationToken);
                return View(nameof(CreateRoof), form);
            }

            form.Area = form.Width * form.Height;
            form.AdjustedTemperature = this._buildingEnvelopeService.CalculateTemperatureCoefficient(form.Density, form.Type);

            return await HandleCreateAsync(form, f => f.ToModelFromForm(), cancellationToken);
        }

        // An action for deleting and getting details of building envelopes by room and type
        [HttpGet]
        public async Task<IActionResult> Manage(Guid roomId, string type, CancellationToken cancellationToken = default)
        {
            try
            {
                BuildingEnvelopeModel[] models;
                switch (type)
                {
                    case "OuterWall":
                        models = await this._buildingEnvelopeService.GetOuterWallsByRoomAsync(roomId, cancellationToken);
                        return View(models.Select(x => x.ToView()));

                    case "Opening":
                        models = await this._buildingEnvelopeService.GetOpeningsByRoomAsync(roomId, cancellationToken);
                        return View(models.Select(x => x.ToView()));

                    case "Floor":
                        models = await this._buildingEnvelopeService.GetFloorsByRoomAsync(roomId, cancellationToken);
                        return View(models.Select(x => x.ToView()));

                    case "InternalFence":
                        models = await this._buildingEnvelopeService.GetInternalFencesByRoomAsync(roomId, cancellationToken);
                        return View(models.Select(x => x.ToView()));

                    case "Roof":
                        models = await this._buildingEnvelopeService.GetRoofsByRoomAsync(roomId, cancellationToken);
                        return View(models.Select(x => x.ToView()));

                    default:
                        return this.RedirectToAction("Error", "Home", new { ErrorMessage = $"Type {type} does not exist in current context." });
                }
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Error", "Home", new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await this._buildingEnvelopeService.GetByIdAsync(id, cancellationToken);
                Guid roomId = model.RoomId;

                if(model is OuterWallModel outerWall)
                {
                    OpeningModel[] openings = await _buildingEnvelopeService
                        .GetOpeningsByRoomAndDirectionAsync(roomId, outerWall.Direction, cancellationToken);

                    foreach (var opening in openings)
                    {
                        await this._buildingEnvelopeService.SoftDeleteAsync(opening.Id, cancellationToken);
                    }
                }

                await this._buildingEnvelopeService.SoftDeleteAsync(id, cancellationToken);
                return this.RedirectToAction(nameof(Index), new { id = roomId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = ex.Message });
            }
        }

        // Helper private methods
        private async Task<IActionResult> HandleCreateAsync<TFormModel, TDomainModel>(
            TFormModel form,
            Func<TFormModel, TDomainModel> mapper,
            CancellationToken cancellationToken = default)
            where TFormModel : BuildingEnvelopeFormModel
            where TDomainModel : BuildingEnvelopeModel
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);

                var model = mapper(form);

                await _buildingEnvelopeService.CreateAsync(model, cancellationToken);

                return RedirectToAction(nameof(Index), new { id = form.RoomId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(form);
            }
        }
        private async Task<List<SelectListItem>> InitializeMaterials(CancellationToken cancellationToken)
        {
            var materials = await _materialService.GetAllAsReadOnlyAsync(new(), cancellationToken);
            return [.. materials.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Type
            })];
        }

        private static List<SelectListItem> InitializeDirections()
            => [.. Enum.GetValues(typeof(Direction))
                .Cast<Direction>()
                .Select(d => new SelectListItem
                {
                    Value = ((int)d).ToString(),
                    Text = d.ToString()
                })];

        private async Task SeedFormDropdownsAsync(BuildingEnvelopeFormModel form, CancellationToken cancellationToken)
        {
            form.Materials = await InitializeMaterials(cancellationToken);

            switch (form)
            {
                case OuterWallFormModel outerWallForm:
                    outerWallForm.Directions = InitializeDirections();
                    break;
                case OpeningFormModel openingForm:
                    openingForm.Directions = InitializeDirections();
                    break;
            }
        }
    }
}
