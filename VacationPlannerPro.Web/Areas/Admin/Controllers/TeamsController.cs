using Microsoft.AspNetCore.Mvc;
using VacationPlannerPro.Business.DTOs.TeamDTOs;
using VacationPlannerPro.Business.Interfaces;

namespace VacationPlannerPro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var teams = await _teamService.GetTeamsAsync(page, pageSize, searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(teams);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var teamWithWorkers = await _teamService.GetTeamWithWorkersByIdAsync(id);
            if (teamWithWorkers == null) return NotFound();

            return View(teamWithWorkers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeamDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _teamService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();

            var updateTeamDto = new UpdateTeamDTO
            {
                Id = team.Id,
                Name = team.Name,
                ProjectId = team.ProjectId,
                LeaderId = team.LeaderId,
                WorkerIds = team.Workers.Select(w => w.Id).ToList(),
            };

            return View(updateTeamDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateTeamDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _teamService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _teamService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
