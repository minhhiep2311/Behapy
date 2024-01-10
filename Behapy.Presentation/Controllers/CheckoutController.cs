using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Constants;
using Behapy.Presentation.Extensions;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Behapy.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Controllers;

public class CheckoutController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly ICustomerService _customerService;
    private readonly ICartService _cartService;
    private readonly UserManager<User> _userManager;

    public CheckoutController(BehapyDbContext context,
        ICustomerService customerService,
        ICartService cartService,
        UserManager<User> userManager)
    {
        _context = context;
        _customerService = customerService;
        _cartService = cartService;
        _userManager = userManager;
    }

    // GET: Checkout
    [Authorize]
    public async Task<IActionResult> Index(string? promotionVoucher = null)
    {
        var user = await _userManager.GetUserAsync(User);
        var customer = _customerService.GetCustomer();
        var promotionId = HttpContext.Session.GetInt32("PromotionId");
        var promotion = promotionId != null
            ? _context.Promotions
                .Include(p => p.ProductPromotions)
                .FirstOrDefault(p => p.Id == promotionId)
            : null;
        var cartItems = await _cartService.GetItems(User) ?? new List<CartItem>();
        var total = cartItems.Sum(item => item.Product.Price * item.Amount);
        var promotionValue = promotion?.CalculateValue(total, cartItems) ?? 0;
        var totalAfterPromotion = CalculateMoney(cartItems, promotion);

        ViewData["PaymentTypes"] = new SelectList(_context.PaymentTypes, "Id", "Name");
        ViewData["CartItems"] = cartItems;
        ViewData["Total"] = total;
        ViewData["PromotionValue"] = promotionValue;
        ViewData["TotalAfterPromotion"] = totalAfterPromotion;

        return View(new CheckoutViewModel
        {
            FullName = user.FullName,
            Address = customer.Address ?? "",
            UseAnotherAddress = false,
            AnotherAddress = ""
        });
    }

    // POST: Checkout
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(
        [Bind("FullName,Address,PaymentTypeId,UseAnotherAddress,AnotherAddress,Note")]
        CheckoutViewModel checkoutViewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewData["PaymentTypes"] = new SelectList(_context.PaymentTypes, "Id", "Name");
            return View(checkoutViewModel);
        }

        var promotionId = HttpContext.Session.GetInt32("PromotionId");
        var promotion = promotionId != null
            ? _context.Promotions
                .Include(p => p.ProductPromotions)
                .FirstOrDefault(p => p.Id == promotionId)
            : null;
        var customer = _customerService.GetCustomer();
        var cartItems = (await _cartService.GetItems(User))!;
        var address = checkoutViewModel.UseAnotherAddress
            ? checkoutViewModel.AnotherAddress
            : checkoutViewModel.Address;
        var total = CalculateMoney(cartItems, promotion);

        var order = new Order
        {
            CustomerId = customer.Id,
            Address = address ?? "",
            Note = checkoutViewModel.Note,
            Promotion = promotion,
            CurrentStatus = OrderStatusConstant.NeedToConfirm,
            PaymentTypeId = checkoutViewModel.PaymentTypeId,
            CreateAt = DateTime.Now,
            TotalMoney = total,
            Discount = cartItems.Sum(item => item.Product.Price * item.Amount) - total,
            OrderDetails = cartItems.Select((ci, idx) => new OrderDetail
            {
                ProductId = ci.ProductId,
                Amount = ci.Amount,
                Price = ci.Product.Price,
                OrderNumber = idx + 1
            }).ToList(),
            OrderStatuses = new List<OrderStatus>
            {
                new() { Status = OrderStatusConstant.NeedToConfirm, CreatedAt = DateTime.Now }
            }
        };

        _context.AddRange(order);
        await _context.SaveChangesAsync();

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        return RedirectToAction("Success", "Orders", new { id = order.Id });
    }

    private static decimal CalculateMoney(List<CartItem> cartItems, Promotion? promotion)
    {
        var total = cartItems.Sum(item => item.Product.Price * item.Amount);
        var promotionValue = promotion?.CalculateValue(total, cartItems) ?? 0;
        var totalAfterPromotion = total - promotionValue;

        return totalAfterPromotion;
    }
}