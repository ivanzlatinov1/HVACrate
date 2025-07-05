using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.Projects;
using HVACrate.Domain.ValueObjects;
using HVACrate.Presentation.Extensions;
using HVACrate.Presentation.Models.FormModels;
using HVACrate.Presentation.Models.ViewModels.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HVACrate.GCommon.GlobalConstants.QueryProperties;

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
                QueryParam = ProjectQueryParam,
                Pagination = pagination
            }, this.User.GetId(), cancellationToken);

            ProjectViewModel[] projects = [.. projectModels.Items.Select(x => new ProjectViewModel
            {
                Id = x.Id,
                Name = x.Name,
                LastModified = x.LastModified
            })];

            return View((projects, projectModels.Count, pagination));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectForm form, CancellationToken cancellationToken = default)
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

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(form);
            }
        }
    }
}
