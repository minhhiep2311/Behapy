using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
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
    [Authorize(Roles = "User")]
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

    //GET: Products/Admin
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Admin(int pg)
    {
        var products = _context.Orders
            .Include(o => o.PaymentType)
            .Include(o => o.Customer.User)
            .Include(o => o.Distributor)
            .Include(o => o.Promotion)
            .OrderByDescending(o => o.CreateAt)
            .AsQueryable();

        //Pagination
        if (pg < 1) pg = 1;
        var recsCount = products.Count();
        var pager = new Pager(recsCount, pg);
        var recSkip = (pg - 1) * pager.PageSize;
        ViewBag.Pager = pager;

        // ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        // ViewData["Category"] = categoryId;

        return View(await products.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    // GET: Orders/Details/5
    [Authorize(Roles = "User")]
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
            .ThenInclude(c => c!.User)
            .FirstOrDefault(o => o.Id == id && o.CustomerId == customerId);
        if (order == null)
            return NotFound();

        return View(order);
    }

    // GET: Orders/Success/5
    [Authorize(Roles = "User")]
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