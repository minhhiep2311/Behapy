using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Services.Interfaces;

namespace Behapy.Presentation.Services.Implementations;

public class PromotionService : IPromotionService
{
    private readonly BehapyDbContext _context;

    public PromotionService(BehapyDbContext context)
    {
        _context = context;
    }

    public List<string> GetAllContent(bool getHidden = false)
    {
        return _context.Promotions
            .Where(
                p => p.EndAt > DateTime.Now &&
                     (getHidden || !p.IsHidden)
            )
            // .Include(p => p.Categories)
            .OrderBy(p => p.StartAt)
            .Select(p => p.Content)
            .ToList();
    }
}