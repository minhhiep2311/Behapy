using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly BehapyDbContext _context;

        public ServiceService(BehapyDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceCategory>> GetCategoryServices()
        {
            return await _context.ServiceCategories.ToListAsync();
        }
    }
}
