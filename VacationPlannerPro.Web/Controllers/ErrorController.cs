using Microsoft.AspNetCore.Mvc;

namespace VacationPlannerPro.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Handle")]
        public IActionResult Handle()
        {
            return View("Error500");
        }

        [Route("Error/StatusCode")]
        public IActionResult StatusCodeHandler(int code)
        {
            if (code == 404)
                return View("Error404");

            return View("ErrorGeneric");
        }
    }
}
