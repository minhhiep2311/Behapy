using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Data;
using Behapy.Presentation.Models;

namespace Behapy.Presentation.Controllers;

public class ProductsController : Controller
{
	private readonly BehapyDbContext _context;

	public ProductsController(BehapyDbContext context)
	{
		_context = context;
	}

	// GET: Products
	public async Task<IActionResult> Index()
	{
		var behapyDbContext = _context.Products.Include(p => p.Category).Include(p => p.Promotion);
		return View(await behapyDbContext.ToListAsync());
	}

	// GET: Products/Details/5
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null || _context.Products == null)
		{
			return NotFound();
		}

		var product = await _context.Products
			.Include(p => p.Category)
			.Include(p => p.Promotion)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (product == null)
		{
			return NotFound();
		}

		return View(product);
	}

	// GET: Products/Create
	public IActionResult Create()
	{
		ViewData["Category"] = new SelectList(_context.Categories, null, "Name");

		Product model = new();
		model.IsActive = true;

		return View(model);
	}

	// POST: Products/Create
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name,Price,IsActive,Description,ImageUrl,Discount,CreatedAt,Category,PromotionId")] Product product)
	{
		if (ModelState.IsValid)
		{
			_context.Add(product);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

		return View(product);
	}

	// GET: Products/Edit/5
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null || _context.Products == null)
		{
			return NotFound();
		}

		var product = await _context.Products.FindAsync(id);
		if (product == null)
		{
			return NotFound();
		}
		ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
		ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id", product.PromotionId);
		return View(product);
	}

	// POST: Products/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,IsActive,Description,ImageUrl,Discount,CreatedAt,CategoryId,PromotionId")] Product product)
	{
		if (id != product.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			try
			{
				_context.Update(product);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(product.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToAction(nameof(Index));
		}
		ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
		ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id", product.PromotionId);
		return View(product);
	}

	// GET: Products/Delete/5
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null || _context.Products == null)
		{
			return NotFound();
		}

		var product = await _context.Products
			.Include(p => p.Category)
			.Include(p => p.Promotion)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (product == null)
		{
			return NotFound();
		}

		return View(product);
	}

	// POST: Products/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		if (_context.Products == null)
		{
			return Problem("Entity set 'BehapyDbContext.Products'  is null.");
		}
		var product = await _context.Products.FindAsync(id);
		if (product != null)
		{
			_context.Products.Remove(product);
		}

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool ProductExists(int id)
	{
		return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
	}
}
