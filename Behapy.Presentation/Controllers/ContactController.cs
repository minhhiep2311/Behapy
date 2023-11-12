using Microsoft.AspNetCore.Mvc;

namespace Behapy.Presentation.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
