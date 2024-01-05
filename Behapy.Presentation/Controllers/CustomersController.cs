using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Microsoft.AspNetCore.Authorization;

namespace Behapy.Presentation.Controllers;

[Authorize(Roles = "Admin,Employee")]
public class CustomersController : Controller
{
    private readonly BehapyDbContext _context;

    public CustomersController(BehapyDbContext context)
    {
        _context = context;
    }

    // GET: Customers
    public async Task<IActionResult> Index([FromQuery(Name = "q")] string? searchText, int pg = 1)
    {
        ViewData["SearchText"] = searchText;

        var customers = _context.Customers
            .Include(c => c.User)
            .Where(p => string.IsNullOrWhiteSpace(searchText) || p.User.FullName.Contains(searchText) || p.User.Email.Contains(searchText) || p.User.PhoneNumber.Contains(searchText));

        const int pageSize = 8;

        if (pg < 1) pg = 1;
        var recsCount = customers.Count();
        var pager = new Pager(recsCount, pg, pageSize);
        var recSkip = (pg - 1) * pageSize;
        ViewBag.Pager = pager;

        return View(await customers.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    // POST: Customers/Search
    [HttpPost]
    public IActionResult Search(string q)
    {
        return RedirectToAction("Index", new { q });
    }

    // GET: Customers/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _context.Customers
            .Include(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: Customers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Address,Birthday,UserId")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", customer.UserId);
        return View(customer);
    }

    // GET: Customers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return NotFound();

        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", customer.UserId);
        return View(customer);
    }

    // POST: Customers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Birthday,UserId")] Customer customer)
    {
        if (id != customer.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", customer.UserId);
        return View(customer);
    }

    // GET: Customers/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _context.Customers
            .Include(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    // POST: Customers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
            _context.Customers.Remove(customer);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CustomerExists(int id)
    {
        return _context.Customers.Any(e => e.Id == id);
    }
}