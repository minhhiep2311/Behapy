using Microsoft.AspNetCore.Mvc;

namespace Behapy.Presentation.Views.Shared.Components.AccountComponent;

public class AccountComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync() => Task.FromResult<IViewComponentResult>(View());
}