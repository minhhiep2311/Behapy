using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Areas.Identity.Data;

namespace Behapy.Presentation.Controllers
{
    public class ServicesController : Controller
    {
        private readonly BehapyDbContext _context;

        public ServicesController(BehapyDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var serviceCategories = await _context.ServiceCategories.ToListAsync();
            return View(serviceCategories);
        }

        //GET: Service by Category
        public async Task<IActionResult> ServicesByCategory(int categoryId)
        {
            var servicesInCategory = await _context.Services
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();

            return View(servicesInCategory);
        }


        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var service = await _context.Services
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
                return NotFound();

            return View(service);
        }
    }
}
