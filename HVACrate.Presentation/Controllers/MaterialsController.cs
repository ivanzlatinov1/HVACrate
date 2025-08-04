using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models.Materials;
using HVACrate.Presentation.Models.Materials;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class MaterialsController(IMaterialService materialService) : Controller
    {
        private readonly IMaterialService _materialService = materialService;

        [HttpGet]
        public IActionResult Create(Guid buildingEnvelopeRoomId, string buildingEnvelopeType)
        {
            MaterialFormModel form = new()
            {
                BuildingEnvelopeRoomId = buildingEnvelopeRoomId,
                BuildingEnvelopeType = buildingEnvelopeType
            };

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialFormModel form, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);

                MaterialModel model = form.ToModel();

                await _materialService.CreateAsync(model, cancellationToken);

                return this.RedirectToAction(nameof(Create), "BuildingEnvelopes",
                    new { roomId = form.BuildingEnvelopeRoomId, type = form.BuildingEnvelopeType });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(form);
            }
        }
    }
}
