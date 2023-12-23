﻿using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly BehapyDbContext _context;

        public DashboardController(BehapyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var today = DateTime.Now;

            ViewData["Revenue"] = _context.Orders
                .Sum(o => o.TotalMoney);
            ViewData["OrdersCount"] = _context.Orders
                .Count(o => o.CurrentStatus != OrderStatusConstant.Denied)
                .ToString("N0");
            ViewData["CustomersCount"] = _context.Customers.Count().ToString("N0");
            ViewData["DistributorsCount"] = _context.Distributors.Count().ToString("N0");
            ViewData["RevenueChart"] = _context.Orders
                .Where(o => o.CreateAt.Year == today.Year)
                .GroupBy(o => new { o.CreateAt.Month, o.CreateAt.Year, IsDistributor = o.DistributorId != null })
                .Select(my => new
                {
                    my.Key.Month,
                    my.Key.IsDistributor,
                    Sum = my.Sum(o => o.TotalMoney)
                });
            ViewData["BestSeller"] = _context.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.CurrentStatus != OrderStatusConstant.Denied)
                .GroupBy(od => od.ProductId)
                .Select(x => new
                {
                    x.Key,
                    Sum = x.Sum(od => od.Price),
                    Count = x.Count()
                })
                .OrderBy(x => x.Count)
                .Take(5)
                .Join(_context.Products,
                    arg => arg.Key,
                    product => product.Id,
                    (arg, product) => new { Product = product, arg.Sum, arg.Count });

            return View();
        }
    }
}