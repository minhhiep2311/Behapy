using Behapy.Presentation.Models;

namespace Behapy.Presentation.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetLatestProducts();
    }
}
