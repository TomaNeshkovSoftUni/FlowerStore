using FlowerShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _products;

    public HomeController(IProductService products)
    {
        _products = products;
    }

    public async Task<IActionResult> Index()
    {
        var latest = await _products.GetLatestAsync(4);
        return View(latest.Select(ProductMapping.ToListItem).ToList());
    }
}
