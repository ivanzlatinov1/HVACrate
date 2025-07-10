using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.Projects;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Extensions;
using HVACrate.Presentation.Models.FormModels;
using HVACrate.Presentation.Models.ViewModels.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Presentation.Controllers
{
    [Authorize(Roles = "User")]
    public class ProjectsController(IProjectService projectService) : Controller
    {
        private readonly IProjectService _projectService = projectService;

        [HttpGet]
        public async Task<IActionResult> Index(BaseQueryFormModel query, CancellationToken cancellationToken = default)
        {
            Pagination pagination = new(Page: query.Page, Limit: query.Limit);

            Result<ProjectModel> projectModels = await this._projectService.GetAllAsReadOnlyAsync(new()
            {
                SearchParam = query.SearchParam,
                QueryParam = QueryProperties.ProjectQueryParam,
                Pagination = pagination
            }, this.User.GetId(), cancellationToken);

            ProjectViewModel[] projects = [.. projectModels.Items.Select(x => new ProjectViewModel
            {
                Id = x.Id,
                Name = x.Name,
                LastModified = x.LastModified.ToLocalTime().ToString(DescriptiveDateFormat)
            })];

            return View((projects, projectModels.Count, pagination));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectFormModel form, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);

                Guid? userId = this.User.GetId();

                if (userId == null)
                    return Unauthorized();

                ProjectModel model = new()
                {
                    Name = form.Name,
                    RegionTemperature = form.RegionTemperature,
                    HVACUserId = (Guid)userId,
                };

                await _projectService.CreateAsync(model, cancellationToken);

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

            ProjectModel project = await this._projectService.GetByIdAsync(id.Value, cancellationToken);

            ProjectFormModel form = new()
            {
                Id = project.Id,
                Name = project.Name,
                RegionTemperature = project.RegionTemperature,
            };

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectFormModel form, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectModel model = await this._projectService.GetByIdAsync(form.Id, cancellationToken);
                    model.Name = form.Name;
                    model.RegionTemperature = form.RegionTemperature;

                    await this._projectService.UpdateAsync(model, cancellationToken);
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

            ProjectModel? project = await this._projectService.GetByIdAsync(id.Value, cancellationToken);

            ProjectViewModel projectViewModel = new()
            {
                Id = project.Id,
                Name = project.Name,
                LastModified = project.LastModified.ToLocalTime().ToString(DescriptiveDateFormat)
            };

            return View(projectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProjectViewModel project, CancellationToken cancellationToken = default)
        {
            try
            {
                if (project != null)
                {
                    await this._projectService.SoftDeleteAsync(project.Id, cancellationToken);
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