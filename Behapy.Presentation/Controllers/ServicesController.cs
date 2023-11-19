using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;

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
            //var behapyDbContext = _context.Services.Include(s => s.Category);
            //return View(await behapyDbContext.ToListAsync());

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
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
    }
}
