using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Controllers;

public class OrdersController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly ICustomerService _customerService;

    public OrdersController(BehapyDbContext context, ICustomerService customerService)
    {
        _context = context;
        _customerService = customerService;
    }

    // GET: Orders
    [Authorize]
    public IActionResult Index()
    {
        var customerId = _customerService.GetCustomer().Id;
        var items = _context.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderDetails)
            .ToList();
        return View(items);
    }

    // GET: Orders/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
            return NotFound();

        var customerId = _customerService.GetCustomer().Id;
        var order = _context.Orders
            .AsNoTracking()
            .Include(o => o.PaymentType)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .ThenInclude(c => c.User)
            .FirstOrDefault(o => o.Id == id && o.CustomerId == customerId);
        if (order == null)
            return NotFound();

        return View(order);
    }

    // GET: Orders/Success/5
    public IActionResult Success(int? id)
    {
        if (id == null)
            return NotFound();

        var customerId = _customerService.GetCustomer().Id;
        var order = _context.Orders
            .AsNoTracking()
            .FirstOrDefault(o => o.Id == id && o.CustomerId == customerId);
        if (order == null)
            return NotFound();

        return View(order);
    }
}