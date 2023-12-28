using AspNetCoreHero.ToastNotification.Abstractions;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Enums;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Controllers;

public class ProductsController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly IFileService _fileService;
    private readonly INotyfService _notyfService;

    public ProductsController(BehapyDbContext context, IFileService fileService, INotyfService notyfService)
    {
        _context = context;
        _fileService = fileService;
        _notyfService = notyfService;
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
            .Include(p => p.ProductPromotions)
            .Where(p =>
                (categoryId == null || p.CategoryId == categoryId) &&
                (string.IsNullOrWhiteSpace(searchText) || EF.Functions.Like(p.Name, $"%{searchText}%"))
            );

        var categoryProductCount = await products.CountAsync();
        var totalProducts = await _context.Products.CountAsync();

        products = sortOrder switch
        {
            "latest" => products.OrderByDescending(p => p.CreatedAt),
            "high-price" => products.OrderByDescending(p => p.Price),
            "low-price" => products.OrderBy(p => p.Price),
            _ => products
        };

        const int pageSize = 8;

        if (pg < 1) pg = 1;
        var recsCount = products.Count();
        var pager = new Pager(recsCount, pg, pageSize);
        var recSkip = (pg - 1) * pageSize;
        ViewBag.Pager = pager;

        ViewBag.CategoryProductCount = categoryProductCount;
        ViewBag.TotalProducts = totalProducts;

        return View(await products.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    //GET: Products/Admin
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Admin(int pg, int? categoryId)
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPromotions)
            .OrderByDescending(p => p.CreatedAt)
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

    //GET: Products/Admin
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> AdminList(int pg, int? categoryId)
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPromotions)
            .OrderByDescending(p => p.CreatedAt)
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

        ViewData["Category"] = categoryId;

        return View(await products.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductPromotions)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
            return NotFound();

        return View(product);
    }


    // GET: Products/Create
    [Authorize(Roles = "Admin,Employee")]
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
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Price,IsActive,Description,ImageUrl,CategoryId,PromotionId,Amount")]
        Product product)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Admin));

        _context.Add(product);
        await _context.SaveChangesAsync();
        _notyfService.Success("Tạo mới thành công!");

        return RedirectToAction(nameof(Admin));
    }


    // GET: Products/Edit/5
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Edit(int? id)
    {
        GetImageKitAuthenticationParameters();

        if (id == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.ProductPromotions)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return NotFound();

        var promotions = _context.Promotions.Where(p => p.Type == PromotionType.Product);

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        ViewData["ProductPromotionsId"] = new MultiSelectList(promotions, "Id", "Name",
            product.ProductPromotions.Select(pp => pp.PromotionId));

        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Price,IsActive,Description,ImageUrl,Discount,CreatedAt,CategoryId,ProductPromotionsId,Amount")]
        Product product)
    {
        if (id != product.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Admin));

        var oldProduct = _context.Products
            .Include(p => p.ProductPromotions)
            .FirstOrDefault(p => p.Id == id);
        if (oldProduct == null)
            return RedirectToAction(nameof(Admin));

        try
        {
            var newProductPromotionsId = product.ProductPromotionsId
                .Select(ppi => new ProductPromotion { ProductId = product.Id, PromotionId = ppi })
                .ToList();

            oldProduct.ProductPromotions.RemoveAll(pp => !newProductPromotionsId.Contains(pp));
            oldProduct.ProductPromotions.AddRange(newProductPromotionsId.Where(pp => !oldProduct.ProductPromotions.Contains(pp)));
            oldProduct.Name = product.Name;
            oldProduct.Amount = product.Amount;
            oldProduct.Price = product.Price;
            oldProduct.CreatedAt = DateTime.Now;
            oldProduct.CategoryId = product.CategoryId;
            oldProduct.IsActive = product.IsActive;
            oldProduct.ImageUrl = product.ImageUrl;
            oldProduct.Description = product.Description;

            _context.Update(oldProduct);
            await _context.SaveChangesAsync();
            _notyfService.Success("Cập nhật thành công!");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (ProductExists(product.Id)) throw;
            _notyfService.Error("Có lỗi xảy ra!");
            return NotFound();
        }

        return RedirectToAction(nameof(Admin));
    }

    // POST: Products/Delete/5
    [HttpDelete]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return RedirectToAction(nameof(Admin));

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        _notyfService.Success("Xóa thành công!");

        return RedirectToAction(nameof(Admin));
    }

    // POST: Products/Search
    [HttpPost]
    public IActionResult Search(string category, string q)
    {
        return RedirectToAction("Index", new { category, q });
    }

    //POST: Products/AddQuantity
    [HttpPost]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> AddQuantity(int id, int quantityToAdd)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return RedirectToAction(nameof(Admin));

        // Thêm số lượng mới vào số lượng hiện tại
        product.Amount += quantityToAdd;

        // Cập nhật dữ liệu trong cơ sở dữ liệu
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Admin));
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