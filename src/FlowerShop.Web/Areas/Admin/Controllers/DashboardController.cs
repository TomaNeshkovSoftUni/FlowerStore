using FlowerShop.Data;
using FlowerShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Admin.Controllers;

/// <summary>Начало на административния панел - достъпно само за роля Admin.</summary>
[Area("Admin")]
[Authorize(Roles = RoleNames.Admin)]
public class DashboardController : Controller
{
    private readonly IProductService _products;

    public DashboardController(IProductService products)
    {
        _products = products;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["ProductsCount"] = await _products.CountAsync();
        return View();
    }
}
