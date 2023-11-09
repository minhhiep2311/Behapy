using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Controllers;

public class ProductsController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly IFileService _fileService;

    public ProductsController(BehapyDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    // GET: Products
    public async Task<IActionResult> Index([FromQuery(Name = "category")] int? categoryId,
        [FromQuery(Name = "q")] string? searchText,
        [FromQuery(Name = "sortOrder")] string? sortOrder,
        int pg = 1)
    {
        ViewData["CategoryId"] = categoryId;
        ViewData["SearchText"] = searchText;
        ViewData["SortOrder"] = sortOrder;


        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Promotion)
            .Where(p =>
                (categoryId == null || p.CategoryId == categoryId) &&
                (string.IsNullOrWhiteSpace(searchText) || EF.Functions.Like(p.Name, $"%{searchText}%"))
            );


        switch (sortOrder)
        {
            case "latest":
                products = products.OrderByDescending(p => p.CreatedAt);
                break;
            case "high-price":
                products = products.OrderByDescending(p => p.Price);
                break;
            case "low-price":
                products = products.OrderBy(p => p.Price);
                break;
        }

        const int pageSize = 8;

        if (pg < 1) pg = 1;
        var recsCount = products.Count();
        var pager = new Pager(recsCount, pg, pageSize);
        var recSkip = (pg - 1) * pageSize;
        ViewBag.Pager = pager;

        return View(await products.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    //GET: Products/Admin
    public async Task<IActionResult> Admin(int pg, int? categoryId)
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Promotion)
             .AsQueryable();

        //Filter 
        if (categoryId.HasValue)
        {
            products = products.Where(p => p.CategoryId == categoryId);
        }

        //Pagination
        const int pageSize = 8;

        if (pg < 1) pg = 1;
        var recsCount = products.Count();
        var pager = new Pager(recsCount, pg, pageSize);
        var recSkip = (pg - 1) * pageSize;
        ViewBag.Pager = pager;


        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");

        ViewData["Category"] = categoryId;


        return View(await products.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
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
        GetImageKitAuthenticationParameters();

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");

        Product model = new()
        {
            IsActive = true
        };

        return View(model);
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Price,IsActive,Description,ImageUrl,CategoryId,PromotionId,Amount")]
        Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        GetImageKitAuthenticationParameters();

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Name", product.PromotionId);

        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Name", product.PromotionId);
        return View(product);
    }

    // POST: Products/Edit/5LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Price,IsActive,Description,ImageUrl,Discount,CreatedAt,CategoryId,PromotionId,Amount")]
        Product product)
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

                throw;
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
        if (id == null)
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
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Admin));
    }

    // POST: Products/Search
    [HttpPost]
    public IActionResult Search(string category, string q)
    {
        return RedirectToAction("Index", new { category, q });
    }

    private void GetImageKitAuthenticationParameters()
    {
        var ikAuthParams = _fileService.GetAuthenticationParameters();

        ViewData["Token"] = ikAuthParams.token;
        ViewData["Signature"] = ikAuthParams.signature;
        ViewData["Expire"] = ikAuthParams.expire;
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}