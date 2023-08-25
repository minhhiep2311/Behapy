using System.Security.Claims;
using Behapy.Presentation.Models;

namespace Behapy.Presentation.Services.Interfaces;

public interface ICartService
{
    Task<List<CartItem>?> GetItems();

    Task<List<CartItem>?> GetItems(ClaimsPrincipal userClaimsPrincipal);
}