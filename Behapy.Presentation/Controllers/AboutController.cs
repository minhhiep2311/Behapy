using Microsoft.AspNetCore.Mvc;

namespace Behapy.Presentation.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
