using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Behapy.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var customer = _customerService.GetCustomer();

        ViewData["PaymentTypes"] = new SelectList(_context.PaymentTypes, "Id", "Name");

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
        ViewData["PaymentTypes"] = new SelectList(_context.PaymentTypes, "Id", "Name");
        if (!ModelState.IsValid)
            return View(checkoutViewModel);

        var customer = _customerService.GetCustomer();
        var cartItems = (await _cartService.GetItems(User))!;
        var address = checkoutViewModel.UseAnotherAddress
            ? checkoutViewModel.AnotherAddress
            : checkoutViewModel.Address;

        var order = new Order
        {
            CustomerId = customer.Id,
            Address = address ?? "",
            Note = checkoutViewModel.Note ?? "",
            CurrentStatus = "Need to confirm",
            PaymentTypeId = checkoutViewModel.PaymentTypeId,
            TotalMoney = cartItems.Sum(ci => ci.Product.Price * ci.Amount),
            OrderDetails = cartItems.Select((ci, idx) => new OrderDetail
            {
                ProductId = ci.ProductId,
                Amount = ci.Amount,
                Price = ci.Product.Price * ci.Amount,
                OrderNumber = idx + 1
            }).ToList(),
            OrderStatuses = new List<OrderStatus>
            {
                new() { Status = "Need to confirm", CreatedAt = DateTime.Now }
            },
            CreateAt = DateTime.Now
        };

        _context.AddRange(order);
        await _context.SaveChangesAsync();

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        return RedirectToAction("Success", "Orders", new { id = order.Id });
    }
}