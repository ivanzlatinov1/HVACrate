using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Extensions;
using HVACrate.Presentation.Models.Buildings;
using HVACrate.Presentation.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class BuildingsController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BuildingsController(IBuildingService buildingService,
        IWebHostEnvironment webHostEnvironment)
        {
            _buildingService = buildingService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id, BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return NotFound();

            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<BuildingModel> buildingModels = await this._buildingService.GetAllAsReadOnlyAsync(new()
            {
                SearchParam = query.SearchParam,
                QueryParam = QueryProperties.BuildingQueryParam,
                Pagination = pagination
            }, id, cancellationToken);

            BuildingViewModel[] buildings = buildingModels.Items.Select(x => x.ToView()).ToArray();

            return View((id, buildings, buildingModels.Count, pagination));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken = default)
        {
            BuildingModel building = await this._buildingService.GetByIdAsync(id, cancellationToken);

            BuildingDetailsViewModel details = building.ToDetailsView();

            return View(details);
        }

        [HttpGet]
        public IActionResult Create(Guid projectId)
        {
            BuildingFormModel form = new()
            {
                ProjectId = projectId,
            };
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BuildingFormModel form, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);

                if (form.Image != null && form.Image.Length > 0)
                {
                    form.ImageUrl = await this._webHostEnvironment.UploadImageAsync(form.Image, form.Name);
                }

                BuildingModel model = form.ToModel();

                await _buildingService.CreateAsync(model, cancellationToken);

                return this.RedirectToAction(nameof(Index), new { id = form.ProjectId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(form);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken = default)
        {
            if (id == null) return NotFound();

            BuildingModel building = await this._buildingService.GetByIdAsync(id.Value, cancellationToken);

            EditBuildingFormModel form = building.ToEditFormModel();

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBuildingFormModel form, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BuildingModel model = await this._buildingService.GetByIdAsync(form.Id, cancellationToken);
                    model.UpdateFromForm(form);

                    if (form.NewImage != null && form.NewImage.Length > 0)
                    {
                        model.ImageUrl = await this._webHostEnvironment.UploadImageAsync(form.NewImage, form.Name);
                    }

                    await this._buildingService.UpdateAsync(model, cancellationToken);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { ErrorMessage = ex.Message });
                }
                return this.RedirectToAction(nameof(Index), new { id = form.ProjectId });
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

            BuildingModel building = await this._buildingService.GetByIdAsync(id.Value, cancellationToken);

            BuildingDetailsViewModel buildingViewModel = building.ToDetailsView();

            return View(buildingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BuildingViewModel building, CancellationToken cancellationToken = default)
        {
            try
            {
                await this._buildingService.DeleteAsync(building.Id, cancellationToken);
                return this.RedirectToAction(nameof(Index), new { id = building.ProjectId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = ex.Message });
            }
        }
    }
}
