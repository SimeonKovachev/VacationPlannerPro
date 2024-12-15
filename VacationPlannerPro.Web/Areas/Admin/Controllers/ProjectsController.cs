using Microsoft.AspNetCore.Mvc;
using VacationPlannerPro.Business.DTOs.ProjectDTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Business.Services;

namespace VacationPlannerPro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllAsync();
            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectDTO createProjectDto)
        {
            if (!ModelState.IsValid)
                return View(createProjectDto);

            await _projectService.CreateAsync(createProjectDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            var updateDto = new UpdateProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProjectDTO updateProjectDto)
        {
            if (!ModelState.IsValid)
                return View(updateProjectDto);

            await _projectService.UpdateAsync(updateProjectDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _projectService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var professions = await _projectService.GetAllAsync();
            return Json(professions);
        }
    }
}
