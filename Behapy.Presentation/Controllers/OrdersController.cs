using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Constants;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    // GET: Promotion/Create
    public IActionResult Create()
    {
        ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Id");
        return View();
    }

    // POST: Promotion/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Value,Unit,Voucher,MaxDiscount,EndAt,StartAt,TypeId")]
        Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Id", order.PaymentType);
        ViewData["TypeId"] = new SelectList(_context.Customers, "Id", "Id", order.Customer);
        ViewData["DistributorId"] = new SelectList(_context.Distributors, "Id", "Id", order.Distributor);
        ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id", order.Promotion);

        return View(order);
    }

    //GET: Orders/Admin
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Admin(int pg)
    {
        var products = _context.Orders
            .Include(o => o.PaymentType)
            .Include(o => o.Customer!.User)
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

        return View(await products.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    // GET: Orders/AdminDetails/5
    [Authorize(Roles = "Admin,Employee")]
    public IActionResult AdminDetails(int? id)
    {
        if (id == null)
            return NotFound();

        var order = _context.Orders
            .AsNoTracking()
            .Include(o => o.PaymentType)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .ThenInclude(c => c!.User)
            .FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound();

        return View(order);
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

    // POST: Orders/UpdateStatus
    [HttpPost]
    [Authorize(Roles = "Admin,Employee")]
    public void UpdateStatus(int id, string status)
    {
        if (!OrderStatusConstant.ValidStatus(status))
            throw new Exception("Invalid status");

        var order = _context.Orders
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);
        if (order == null)
            throw new Exception("Product not found");

        UpdateStatus(order, status);

        _context.Update(order);
        _context.SaveChanges();
    }

    private static void UpdateStatus(Order order, string status)
    {
        switch (order.CurrentStatus)
        {
            case OrderStatusConstant.NeedToConfirm:
            {
                if (status != OrderStatusConstant.Confirmed && status != OrderStatusConstant.Denied)
                    throw new Exception("Invalid status: Status should be Confirmed or Denied");
                break;
            }
            default:
                throw new Exception("Unhandled status: " + status);
        }

        var orderStatus = new OrderStatus
        {
            CreatedAt = DateTime.Now,
            Status = status
        };

        order.CurrentStatus = status;
        order.OrderStatuses.Add(orderStatus);
    }
}