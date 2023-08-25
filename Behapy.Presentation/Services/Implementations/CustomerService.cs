using System.Security.Claims;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly BehapyDbContext _context;

    public CustomerService(IHttpContextAccessor contextAccessor, BehapyDbContext context)
    {
        _contextAccessor = contextAccessor;
        _context = context;
    }

    public void Register(string userId)
    {
        var customer = new Customer { UserId = userId };
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public Customer? GetCustomer()
    {
        var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;
        if (userId == null) return null;

        return _context.Customers
            .AsNoTracking()
            .FirstOrDefault(c => c.UserId == userId);
    }
}