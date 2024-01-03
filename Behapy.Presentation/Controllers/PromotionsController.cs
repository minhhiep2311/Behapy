using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Enums;
using Behapy.Presentation.Extensions;
using Behapy.Presentation.Models;
using Microsoft.AspNetCore.Authorization;

namespace Behapy.Presentation.Controllers;

[Authorize(Roles = "Admin,Employee")]
public class PromotionsController : Controller
{
    private readonly BehapyDbContext _context;

    public PromotionsController(BehapyDbContext context)
    {
        _context = context;
    }

    // GET: Promotions
    public async Task<IActionResult> Index(int pg)
    {
        var promotions = _context.Promotions
            .OrderByDescending(o => o.Id)
            .AsQueryable();

        //Pagination
        if (pg < 1) pg = 1;
        var recsCount = promotions.Count();
        var pager = new Pager(recsCount, pg);
        var recSkip = (pg - 1) * pager.PageSize;
        ViewBag.Pager = pager;

        return View(await promotions.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    // GET: Promotions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var promotion = await _context.Promotions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (promotion == null)
            return NotFound();

        return View(promotion);
    }

    // GET: Promotions/Create
    public IActionResult Create()
    {
        var units =
            from PromotionUnit u in Enum.GetValues(typeof(PromotionUnit))
            select new { ID = (int)u, Name = u.GetName() };
        var types =
            from PromotionType u in Enum.GetValues(typeof(PromotionType))
            select new { ID = (int)u, Name = u.GetName() };

        ViewData["Type"] = new SelectList(types, "ID", "Name");
        ViewData["Unit"] = new SelectList(units, "ID", "Name");

        return View();
    }

    // POST: Promotion/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Value,Unit,Voucher,MaxDiscount,EndAt,StartAt,Type,MinOrderValue")]
        Promotion promotion)
    {
        if (ModelState.IsValid)
        {
            _context.Add(promotion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var units =
            from PromotionUnit u in Enum.GetValues(typeof(PromotionUnit))
            select new { ID = (int)u, Name = u.GetName() };
        var types =
            from PromotionType u in Enum.GetValues(typeof(PromotionType))
            select new { ID = (int)u, Name = u.GetName() };

        ViewData["Type"] = new SelectList(types, "ID", "Name");
        ViewData["Unit"] = new SelectList(units, "ID", "Name");

        return View(promotion);
    }

    // GET: Promotion/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var promotion = await _context.Promotions.FindAsync(id);
        if (promotion == null)
            return NotFound();

        var units =
            from PromotionUnit u in Enum.GetValues(typeof(PromotionUnit))
            select new { ID = (int)u, Name = u.GetName() };
        var types =
            from PromotionType u in Enum.GetValues(typeof(PromotionType))
            select new { ID = (int)u, Name = u.GetName() };

        ViewData["Type"] = new SelectList(types, "ID", "Name");
        ViewData["Unit"] = new SelectList(units, "ID", "Name");

        return View(promotion);
    }

    // POST: Promotion/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Value,Unit,Voucher,MaxDiscount,EndAt,StartAt,Type,MinOrderValue")]
        Promotion promotion)
    {
        if (id != promotion.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(promotion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(promotion.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        var units =
            from PromotionUnit u in Enum.GetValues(typeof(PromotionUnit))
            select new { ID = (int)u, Name = u.GetName() };
        var types =
            from PromotionType u in Enum.GetValues(typeof(PromotionType))
            select new { ID = (int)u, Name = u.GetName() };

        ViewData["Type"] = new SelectList(types, "ID", "Name");
        ViewData["Unit"] = new SelectList(units, "ID", "Name");

        return View(promotion);
    }

    // GET: Promotion/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var promotion = await _context.Promotions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (promotion == null)
            return NotFound();

        return View(promotion);
    }

    // POST: Promotion/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var promotion = await _context.Promotions.FindAsync(id);
        if (promotion != null)
            _context.Promotions.Remove(promotion);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PromotionExists(int id)
    {
        return _context.Promotions.Any(e => e.Id == id);
    }
}