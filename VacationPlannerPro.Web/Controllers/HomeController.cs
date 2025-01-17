using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VacationPlannerPro.Web.Models;

namespace VacationPlannerPro.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("admin")]
        public IActionResult Admin()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
