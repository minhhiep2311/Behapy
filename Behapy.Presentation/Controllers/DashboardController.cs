using Microsoft.AspNetCore.Mvc;

namespace Behapy.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
