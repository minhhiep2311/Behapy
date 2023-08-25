using System.Security.Claims;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Services.Implementations;

public class CartService : ICartService
{
    private readonly BehapyDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public CartService(BehapyDbContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
    }

    public Task<List<CartItem>?> GetItems()
    {
        return GetItems(_contextAccessor.HttpContext?.User);
    }

    public async Task<List<CartItem>?> GetItems(ClaimsPrincipal? userClaimsPrincipal)
    {
        if (!_signInManager.IsSignedIn(userClaimsPrincipal))
        {
            return null;
        }

        var user = await _userManager.GetUserAsync(userClaimsPrincipal);
        var customerId = _context.Customers
            .AsNoTracking()
            .FirstOrDefault(c => c.UserId == user.Id)?.Id;

        if (customerId == null)
        {
            return new List<CartItem>();
        }

        var cartItems = _context.CartItems
            .Where(c => c.CustomerId == customerId)
            .Include(c => c.Product)
            .AsNoTracking()
            .ToList();

        return cartItems;
    }
}