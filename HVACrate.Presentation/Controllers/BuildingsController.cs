using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Models.FormModels;
using HVACrate.Presentation.Models.ViewModels.Buildings;
using HVACrate.Presentation.Models.ViewModels.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants.QueryProperties;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class BuildingsController(IBuildingService buildingService) : Controller
    {
        private readonly IBuildingService _buildingService = buildingService;

        [HttpGet]
        public async Task<IActionResult> Index(BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            Guid projectId = Guid.Parse(Convert.ToString(TempData["ProjectId"]) ?? string.Empty);
            if (projectId == Guid.Empty) return NotFound();

            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<BuildingModel> buildingModels = await this._buildingService.GetAllAsReadOnlyAsync(new()
            {
                SearchParam = query.SearchParam,
                QueryParam = BuildingQueryParam,
                Pagination = pagination
            }, projectId, cancellationToken);

            BuildingViewModel[] buildings = [.. buildingModels.Items.Select(x => new BuildingViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                ImageUrl = x.ImageUrl
            })];

            return View((projectId, buildings, buildingModels.Count, pagination));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            BuildingModel building = await this._buildingService.GetByIdAsync(id, cancellationToken);

            BuildingDetailsViewModel details = new()
            {
                Id = building.Id,
                Name = building.Name,
                Location = building.Location,
                ImageUrl = building.ImageUrl,
                TotalHeight = building.TotalHeight,
                Floors = building.Floors,
                Orientation = building.Orientation,
                ProjectId = building.ProjectId,
                ProjectName = building.Project.Name,
                WindSpeed = building.WindSpeed
            };

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

                BuildingModel model = new()
                {
                    Name = form.Name,
                    WindSpeed = form.WindSpeed,
                    Floors = form.Floors,
                    ImageUrl = form.ImageUrl,
                    Location = form.Location,
                    Orientation = form.Orientation,
                    TotalHeight = form.TotalHeight,
                    ProjectId = form.ProjectId,
                };

                await _buildingService.CreateAsync(model, cancellationToken);

                TempData["ProjectId"] = form.ProjectId;
                return this.RedirectToAction(nameof(Index));
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

            BuildingFormModel form = new()
            {
                Id = building.Id,
                Name = building.Name,
                WindSpeed = building.WindSpeed,
                Floors = building.Floors,
                ImageUrl = building.ImageUrl,
                Location = building.Location,
                Orientation = building.Orientation,
                TotalHeight = building.TotalHeight,
                ProjectId = building.ProjectId,
            };

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BuildingFormModel form, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BuildingModel model = await this._buildingService.GetByIdAsync(id, cancellationToken);
                    model.Name = form.Name;
                    model.WindSpeed = form.WindSpeed;
                    model.Floors = form.Floors;
                    model.ImageUrl = form.ImageUrl;
                    model.Location = form.Location;
                    model.Orientation = form.Orientation;
                    model.TotalHeight = form.TotalHeight;
                    model.ProjectId = form.ProjectId;

                    await this._buildingService.UpdateAsync(model, cancellationToken);
                }
                catch (Exception)
                {
                    // Implement 404 page
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

            BuildingModel? building = await this._buildingService.GetByIdAsync(id.Value, cancellationToken);

            BuildingViewModel buildingViewModel = new()
            {
                Id = building.Id,
                Name = building.Name,
                ImageUrl = building.ImageUrl,
                Location = building.Location,
            };

            return View(buildingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BuildingViewModel building, CancellationToken cancellationToken = default)
        {
            try
            {
                if (building != null)
                {
                    await this._buildingService.SoftDeleteAsync(building.Id, cancellationToken);
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Implement 404 page
                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
