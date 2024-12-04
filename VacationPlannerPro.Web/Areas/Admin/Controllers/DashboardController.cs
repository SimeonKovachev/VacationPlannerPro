using Microsoft.AspNetCore.Mvc;

namespace VacationPlannerPro.Web.Areas.Dashboard.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
