using Microsoft.AspNetCore.Mvc;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.Interfaces;

namespace VacationPlannerPro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfessionsController : Controller
    {
        private readonly IProfessionService _professionService;

        public ProfessionsController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        public async Task<IActionResult> Index(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var pagedResult = await _professionService.GetProfessionsAsync(page, pageSize, searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(pagedResult);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var profession = await _professionService.GetByIdAsync(id);
            if (profession == null) return NotFound();

            return View(profession);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfessionDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _professionService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var profession = await _professionService.GetByIdAsync(id);
            if (profession == null) return NotFound();

            return View(new ProfessionDTO { Id = profession.Id, Name = profession.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfessionDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _professionService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var profession = await _professionService.GetByIdAsync(id);
            if (profession == null) return NotFound();

            return View(profession);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _professionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfessions()
        {
            var professions = await _professionService.GetAllAsync();
            return Json(professions);
        }
    }
}
