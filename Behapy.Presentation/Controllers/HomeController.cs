using Behapy.Presentation.Services.Interfaces;
using Behapy.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Behapy.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IServiceService _serviceService;

    public HomeController(IProductService productService, IServiceService serviceService)
    {
        _productService = productService;
        _serviceService = serviceService;
    }

    public async Task<IActionResult> Index()
    {
        var homeViewModel = new HomeViewModel
        {
            LatestProducts = await _productService.GetLatestProducts(),
            ServiceCategories = await _serviceService.GetCategoryServices()
        };

        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}