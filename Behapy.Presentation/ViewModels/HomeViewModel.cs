using Behapy.Presentation.Models;

namespace Behapy.Presentation.ViewModels;

public class HomeViewModel
{
    public List<Product> LatestProducts { get; set; } = new();
    public List<ServiceCategory> ServiceCategories { get; set; } = new();
}