using System.Net.Mime;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Controllers;

public class CartController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly ICustomerService _customerService;
    private readonly ICartService _cartService;

    public CartController(BehapyDbContext context, ICustomerService customerService, ICartService cartService)
    {
        _context = context;
        _customerService = customerService;
        _cartService = cartService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var items = await GetAll();
        return View(items);
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