using Behapy.Presentation.Models;

namespace Behapy.Presentation.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceCategory>> GetCategoryServices();
    }
}
