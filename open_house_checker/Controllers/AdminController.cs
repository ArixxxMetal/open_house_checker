using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace open_house_checker.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        public IActionResult EmployeesReport()
        {
            return View();
        }

        public IActionResult VisitorsReport()
        {
            return View();
        }
    }
}
