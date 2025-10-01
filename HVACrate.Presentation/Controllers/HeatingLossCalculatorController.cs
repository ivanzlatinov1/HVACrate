using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class HeatingLossCalculatorController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBuildingEnvelopeService _buildingEnvelopeService;

        public HeatingLossCalculatorController(IRoomService roomService, IBuildingEnvelopeService buildingEnvelopeService)
        {
            _roomService = roomService;
            _buildingEnvelopeService = buildingEnvelopeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            RoomModel[] enclosedRooms = await this._roomService.GetAllEnclosedRoomsAsync(pagination, cancellationToken);

            return View((enclosedRooms.Select(x => x.ToCalculatableViewModel()).ToArray(), pagination));
        }

        [HttpGet]
        public async Task<IActionResult> Calculate(Guid roomId, CancellationToken cancellationToken = default)
        {
            List<BuildingEnvelopeModel> buildingEnvelopeModels =
                await this._buildingEnvelopeService.GetAllAsReadOnlyAsync(roomId, cancellationToken);

            double heatingLoss = 0;

            var buildingEnvelopes = buildingEnvelopeModels.Select(x => x.ToCalculatableViewModel()).ToArray();

            for (int i = 0; i < buildingEnvelopeModels.Count; i++)
            {
                double envelopeHeatingLoss = await this._buildingEnvelopeService.CalculateHeatTransmission(buildingEnvelopeModels[i]);
                heatingLoss += envelopeHeatingLoss;
                buildingEnvelopes[i].HeatLoss = envelopeHeatingLoss;
            }

            return View((buildingEnvelopes, heatingLoss));
        }
    }
}
