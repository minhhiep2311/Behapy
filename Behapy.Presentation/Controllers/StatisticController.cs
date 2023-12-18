using Microsoft.AspNetCore.Mvc;
using Behapy.Presentation.Areas.Identity.Data;

namespace Behapy.Presentation.Controllers
{
    public class StatisticController : Controller
    {
        private readonly BehapyDbContext _context;

        public StatisticController(BehapyDbContext context)
        {
            _context = context;
        }

        // GET: Statistic
        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(View());
        }
    }
}
