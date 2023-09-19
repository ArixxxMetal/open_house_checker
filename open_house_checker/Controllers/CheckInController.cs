using Microsoft.AspNetCore.Mvc;

namespace open_house_checker.Controllers
{
    public class CheckInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Stations()
        {
            return View();
        }
    }
}
