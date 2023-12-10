using AspNetCoreHero.ToastNotification.Abstractions;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly BehapyDbContext _context;

        public ProductService(BehapyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetLatestProducts()
        {
            return await _context.Products
               .OrderByDescending(p => p.CreatedAt)
               .ToListAsync();
        }
    }
}
