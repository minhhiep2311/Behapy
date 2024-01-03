using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;

namespace Behapy.Presentation.Controllers
{
    public class DistributorsController : Controller
    {
        private readonly BehapyDbContext _context;

        public DistributorsController(BehapyDbContext context)
        {
            _context = context;
        }

        // GET: Distributors
        public async Task<IActionResult> Index(int pg = 1)
        {
            var distributors = _context.Distributors
                .Include(d => d.DistributorLevel)
                .OrderBy(d => d.DistributorLevelId);

            const int pageSize = 8;

            if (pg < 1) pg = 1;
            var recsCount = distributors.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            var recSkip = (pg - 1) * pageSize;
            ViewBag.Pager = pager;

            return View(await distributors.Skip(recSkip).Take(pager.PageSize).ToListAsync());
        }

        // GET: Distributors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributor = await _context.Distributors
                .Include(d => d.DistributorLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (distributor == null)
            {
                return NotFound();
            }

            return View(distributor);
        }

        // GET: Distributors/Create
        public IActionResult Create()
        {
            ViewData["DistributorLevelId"] = new SelectList(_context.DistributorLevels, "Id", "Name");
            return View();
        }

        // POST: Distributors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Address,Phone,Position")] Distributor distributor)
        {
            if (ModelState.IsValid)
            {
                distributor.DistributorLevelId = 1;
                _context.Add(distributor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DistributorLevelId"] =
                new SelectList(_context.DistributorLevels, "Id", "Name", distributor.DistributorLevelId);
            return View(distributor);
        }

        // GET: Distributors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributor = await _context.Distributors.FindAsync(id);
            if (distributor == null)
            {
                return NotFound();
            }

            ViewData["DistributorLevelId"] =
                new SelectList(_context.DistributorLevels, "Id", "Name", distributor.DistributorLevelId);
            return View(distributor);
        }

        // POST: Distributors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,FullName,Address,Phone,DistributorLevelId")]
            Distributor distributor)
        {
            if (id != distributor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distributor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistributorExists(distributor.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DistributorLevelId"] =
                new SelectList(_context.DistributorLevels, "Id", "Name", distributor.DistributorLevelId);
            return View(distributor);
        }

        // GET: Distributors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributor = await _context.Distributors
                .Include(d => d.DistributorLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (distributor == null)
            {
                return NotFound();
            }

            return View(distributor);
        }

        // POST: Distributors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distributor = await _context.Distributors.FindAsync(id);
            if (distributor != null)
            {
                _context.Distributors.Remove(distributor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistributorExists(int id)
        {
            return _context.Distributors.Any(e => e.Id == id);
        }
    }
}