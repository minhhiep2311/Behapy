using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Services.Interfaces;

namespace Behapy.Presentation.Controllers
{
    public class ServicesController : Controller
    {
        private readonly BehapyDbContext _context;
        private readonly IServiceService _serviceService;


        public ServicesController(BehapyDbContext context, IServiceService serviceService)
        {
            _context = context;
            _serviceService = serviceService;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var serviceCategories = await _serviceService.GetCategoryServices();
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
