using Behapy.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Behapy.Presentation.Views.Shared.Components.MiniCartComponent;

public class MiniCartComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(List<CartItem>? cartItems)
    {
        return Task.FromResult<IViewComponentResult>(View(cartItems));
    }
}