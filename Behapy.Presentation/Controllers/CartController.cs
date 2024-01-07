using System.Net.Mime;
using AspNetCoreHero.ToastNotification.Abstractions;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Behapy.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Controllers;

public class CartController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly ICustomerService _customerService;
    private readonly ICartService _cartService;
    private readonly IPromotionService _promotionService;
    private readonly INotyfService _notyfService;

    public CartController(BehapyDbContext context, ICustomerService customerService, ICartService cartService,
        IPromotionService promotionService, INotyfService notyfService)
    {
        _context = context;
        _customerService = customerService;
        _cartService = cartService;
        _promotionService = promotionService;
        _notyfService = notyfService;
    }

    // GET: Cart
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var items = await GetAll();
        ViewData["Promotions"] = _promotionService.GetAllContent();

        return View(items);
    }

    // POST: Cart
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([Bind("Products,PromotionVoucher")] CartUpdateViewModel model)
    {
        foreach (var product in model.Products.Where(product => product.Amount <= 0))
        {
            model.Products.Remove(product);
        }

        foreach (var product in model.Products
                     .Where(product => _context.Products.FirstOrDefault(p => p.Id == product.Id && p.IsActive) == null))
        {
            _notyfService.Error($"Sản phẩm {product.Name} không còn kinh doanh! Vui lòng xóa khỏi giỏ hàng");
            return await Index();
        }

        var customer = _customerService.GetCustomerOrDefault(true);
        if (customer == null)
        {
            throw new Exception("Not logged in");
        }

        var now = DateTime.Now;
        await _context.Entry(customer)
            .Collection(c => c.CartItems)
            .LoadAsync();
        customer.CartItems.Clear();
        
        foreach (var product in model.Products)
        {
            customer.CartItems.Add(new CartItem
            {
                CustomerId = customer.Id,
                ProductId = product.Id,
                Amount = product.Amount,
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        if (!string.IsNullOrWhiteSpace(model.Voucher))
        {
            var promotion = _context.Promotions
                .FirstOrDefault(p => p.Voucher == model.Voucher);

            if (promotion == null)
            {
                _notyfService.Error("Mã khuyến mãi không hợp lệ");
                return await Index();
            }
        }

        await _context.SaveChangesAsync();

        _notyfService.Success("Cập nhật thành công!");
        return await Index();
    }

    // GET: Cart/GetAll
    [HttpGet]
    public async Task<List<CartItem>?> GetAll()
    {
        return await _cartService.GetItems();
    }

    // POST: Cart/Add/5
    [HttpPost]
    public ContentResult Add(int productId, int amount = 1)
    {
        if (amount <= 0)
        {
            Response.StatusCode = 500;
            return Content("Số lượng phải là số dương", MediaTypeNames.Text.Plain);
        }

        var customer = _customerService.GetCustomerOrDefault();
        if (customer == null)
            throw new Exception("Not logged in");

        var product = _context.Products
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            Response.StatusCode = 500;
            return Content("Không tìm thấy sản phẩm", MediaTypeNames.Text.Plain);
        }

        if (product.Amount < amount)
        {
            Response.StatusCode = 500;
            return Content("Số lượng sản phẩm trong kho không đủ, vui lòng giảm số lượng", MediaTypeNames.Text.Plain);
        }

        var cartItem = _context.CartItems
            .Where(c => c.CustomerId == customer.Id && c.ProductId == productId)
            .Include(c => c.Product)
            .FirstOrDefault();

        if (cartItem != null)
        {
            cartItem.Amount += amount;
        }
        else
        {
            var model = new CartItem
            {
                Amount = amount,
                CustomerId = customer.Id,
                ProductId = product.Id
            };

            _context.CartItems.Add(model);
        }

        _context.SaveChanges();

        return Content("", MediaTypeNames.Text.Plain);
    }

    // DELETE: Cart/Delete/5
    [HttpDelete]
    [Authorize]
    public void Delete(int productId)
    {
        var customer = _customerService.GetCustomerOrDefault();
        if (customer == null) throw new Exception("Not logged in");

        var product = _context.Products
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == productId);
        if (product == null) throw new Exception("Product not found");

        var cartItem = _context.CartItems
            .FirstOrDefault(c => c.CustomerId == customer.Id && c.ProductId == productId);

        if (cartItem == null) return;

        _context.CartItems.Remove(cartItem);
        _context.SaveChanges();
    }
}