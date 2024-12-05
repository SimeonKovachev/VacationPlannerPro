using Microsoft.AspNetCore.Mvc;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Entities;

namespace VacationPlannerPro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LeadersController : Controller
    {
        private readonly ILeaderService _leaderService;

        public LeadersController(ILeaderService leaderService)
        {
            _leaderService = leaderService;
        }

        public async Task<IActionResult> Index()
        {
            var leaders = await _leaderService.GetAllAsync();
            return View(leaders);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var leader = await _leaderService.GetByIdAsync(id);
            if (leader == null) return NotFound();

            return View(leader);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaderDTO leaderDto)
        {
            if (!ModelState.IsValid) return View(leaderDto);

            await _leaderService.CreateAsync(leaderDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var leader = await _leaderService.GetByIdAsync(id);
            if (leader == null) return NotFound();

            var updateLeaderDto = new UpdateLeaderDTO
            {
                Id = leader.Id,
                FullName = leader.FullName,
                Age = leader.Age,
                ProfessionId = leader.ProfessionId
            };

            return View(updateLeaderDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateLeaderDTO leaderDto)
        {
            if (!ModelState.IsValid) return View(leaderDto);

            await _leaderService.UpdateAsync(leaderDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var leader = await _leaderService.GetByIdAsync(id);
            if (leader == null) return NotFound();

            return View(leader);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _leaderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
