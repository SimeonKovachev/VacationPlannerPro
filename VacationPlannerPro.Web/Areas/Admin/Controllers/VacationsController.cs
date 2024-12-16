using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationPlannerPro.Business.DTOs.VacationDTOs;
using VacationPlannerPro.Business.Interfaces;

namespace VacationPlannerPro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class VacationsController : Controller
    {
        private readonly IVacationService _vacationService;

        public VacationsController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            var paginatedVacations = await _vacationService.GetVacationsAsync(pageNumber, pageSize, searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(paginatedVacations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVacationDTO createVacationDto)
        {
            if (!ModelState.IsValid) return View(createVacationDto);

            await _vacationService.CreateAsync(createVacationDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var vacation = await _vacationService.GetByIdAsync(id);
            if (vacation == null) return NotFound();

            var updateVacationDto = new UpdateVacationDTO
            {
                Id = vacation.Id,
                StartDate = vacation.StartDate,
                EndDate = vacation.EndDate,
                Type = vacation.Type,
                WorkerId = vacation.WorkerId
            };

            return View(updateVacationDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateVacationDTO updateVacationDto)
        {
            if (!ModelState.IsValid) return View(updateVacationDto);

            await _vacationService.UpdateAsync(updateVacationDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var vacation = await _vacationService.GetByIdAsync(id);
            if (vacation == null) return NotFound();

            return View(vacation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _vacationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
