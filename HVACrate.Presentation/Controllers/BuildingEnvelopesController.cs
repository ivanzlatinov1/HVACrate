using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.BuildingEnvelopes;
using HVACrate.Presentation.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class BuildingEnvelopesController(IBuildingEnvelopeService buildingEnvelopeService,
        IRoomService roomService) : Controller
    {
        private readonly IBuildingEnvelopeService _buildingEnvelopeService = buildingEnvelopeService;
        private readonly IRoomService _roomService = roomService;

        [HttpGet]
        public async Task<IActionResult> Index(Guid id, BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return NotFound();

            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<BuildingEnvelopeModel> buildingEnvelopeModels = await this._buildingEnvelopeService.GetAllAsReadOnlyAsync(new()
            {
                Pagination = pagination
            }, id, cancellationToken);

            BuildingEnvelopeViewModel[] buildingEnvelopes = [..buildingEnvelopeModels.Items.Select(x => new BuildingEnvelopeViewModel
            {
                Id = x.Id,
                Type = x.Type.ToString(),
                Count = x.Count,
            })];

            string roomNumber = await this._roomService.GetRoomNumberAsync(id, cancellationToken);
            Guid buildingId = await this._roomService.GetBuildingIdAsync(id, cancellationToken);

            return View((id, buildingId, roomNumber, buildingEnvelopes, buildingEnvelopeModels.Count, pagination));
        }

        [HttpGet]
        public IActionResult Create(Guid roomId, string type)
        {
            switch (type)
            {
                case "OuterWall":
                    var outerWallForm = new OuterWallFormModel { RoomId = roomId };
                    return View(nameof(CreateOuterWall), outerWallForm);

                case "Opening":
                    var openingForm = new OpeningFormModel { RoomId = roomId };
                    return View(nameof(CreateOpening), openingForm);

                case "Floor":
                    var floorForm = new FloorFormModel { RoomId = roomId };
                    return View(nameof(CreateFloor), floorForm);

                case "InternalFence":
                    var internalFenceForm = new InternalFenceFormModel { RoomId = roomId };
                    return View(nameof(CreateInternalFence), internalFenceForm);

                case "Roof":
                    var roofForm = new RoofFormModel { RoomId = roomId };
                    return View(nameof(CreateRoof), roofForm);

                default:
                    return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOuterWall(OuterWallFormModel form, CancellationToken cancellationToken = default)
            => await HandleCreateAsync(form, f => new OuterWallModel
            {
                Direction = Enum.Parse<Direction>(f.Direction),
                Height = f.Height,
                Width = f.Width,
                AdjustedTemperature = f.AdjustedTemperature,
                ShouldReduceHeatingArea = f.ShouldReduceHeatingArea,
                Count = f.Count,
                Density = f.Density,
                HeatTransferCoefficient = f.HeatTransferCoefficient,
                RoomId = f.RoomId,
                MaterialId = f.MaterialId
            }, cancellationToken);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOpening(OpeningFormModel form, CancellationToken cancellationToken = default)
            => await HandleCreateAsync(form, f => new OpeningModel
            {
                Direction = Enum.Parse<Direction>(f.Direction),
                Height = f.Height,
                Width = f.Width,
                AdjustedTemperature = f.AdjustedTemperature,
                JointLength = f.JointLength,
                Count = f.Count,
                Density = f.Density,
                VentilationCoefficient = f.VentilationCoefficient,
                HeatTransferCoefficient = f.HeatTransferCoefficient,
                RoomId = f.RoomId,
                MaterialId = f.MaterialId,
            }, cancellationToken);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFloor(FloorFormModel form, CancellationToken cancellationToken = default)
            => await HandleCreateAsync(form, f => new FloorModel
            {
                Height = f.Height,
                Width = f.Width,
                AdjustedTemperature = f.AdjustedTemperature,
                GroundWaterLength = f.GroundWaterLength,
                GroundWaterTemperature = f.GroundWaterTemperature,
                Count = f.Count,
                ThermalConductivityResistance = f.ThermalConductivityResistance,
                Density = f.Density,
                HeatTransferCoefficient = f.HeatTransferCoefficient,
                RoomId = f.RoomId,
                MaterialId = f.MaterialId,
            }, cancellationToken);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInternalFence(InternalFenceFormModel form, CancellationToken cancellationToken = default)
            => await HandleCreateAsync(form, f => new InternalFenceModel
            {
                Height = f.Height,
                Width = f.Width,
                AdjustedTemperature = f.AdjustedTemperature,
                Count = f.Count,
                Density = f.Density,
                HeatTransferCoefficient = f.HeatTransferCoefficient,
                RoomId = f.RoomId,
                MaterialId = f.MaterialId,
            }, cancellationToken);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoof(RoofFormModel form, CancellationToken cancellationToken = default)
            => await HandleCreateAsync(form, f => new RoofModel
            {
                Height = f.Height,
                Width = f.Width,
                AdjustedTemperature = f.AdjustedTemperature,
                Count = f.Count,
                Density = f.Density,
                HeatTransferCoefficient = f.HeatTransferCoefficient,
                RoomId = f.RoomId,
                MaterialId = f.MaterialId,
            }, cancellationToken);

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
    }
}
