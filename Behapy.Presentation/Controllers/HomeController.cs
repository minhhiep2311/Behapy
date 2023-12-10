using AspNetCoreHero.ToastNotification.Abstractions;
using Behapy.Domain.Models;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Implementations;
using Behapy.Presentation.Services.Interfaces;
using Behapy.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Behapy.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly IFileService _fileService;
    private readonly INotyfService _notyfService;
    private readonly IProductService _productService;
    private readonly IServiceService _serviceService;

    public HomeController(BehapyDbContext context, IFileService fileService, INotyfService notyfService, IProductService productService, IServiceService serviceService)
    {
        _context = context;
        _fileService = fileService;
        _notyfService = notyfService;
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