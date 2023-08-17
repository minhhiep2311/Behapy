using Behapy.Presentation.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Views.Shared.Components.SearchComponent;

public class SearchComponent : ViewComponent
{
    private readonly BehapyDbContext _context;

    public SearchComponent(BehapyDbContext context) => _context = context;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var items = await _context.Categories.ToListAsync();
        return View(items);
    }
}