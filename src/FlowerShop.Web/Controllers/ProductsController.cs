using FlowerShop.Services;
using FlowerShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _products;

    public ProductsController(IProductService products)
    {
        _products = products;
    }

    public async Task<IActionResult> Index(int? categoryId, string? search)
    {
        var products = await _products.GetAllAsync(categoryId, search);
        var categories = await _products.GetCategoriesAsync();

        var model = new ProductsIndexViewModel
        {
            Products = products.Select(ProductMapping.ToListItem).ToList(),
            Categories = categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToList(),
            SelectedCategoryId = categoryId,
            Search = search
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _products.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return View(ProductMapping.ToDetails(product));
    }
}
