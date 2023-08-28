using Microsoft.AspNetCore.Mvc;

namespace open_house_checker.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
