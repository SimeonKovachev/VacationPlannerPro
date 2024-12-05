using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAllProfessions()
        {
            var professions = await _professionService.GetAllAsync();
            return Json(professions);
        }
    }
}
