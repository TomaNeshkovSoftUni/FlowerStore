using FlowerShop.Services.Models;
using FlowerShop.Web.Models;

namespace FlowerShop.Web.Controllers;

/// <summary>Преобразуване на DTO обекти от услугите във view модели.</summary>
internal static class ProductMapping
{
    public static ProductListItemViewModel ToListItem(ProductDto p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        ImageUrl = p.ImageUrl,
        CategoryName = p.CategoryName
    };

    public static ProductDetailsViewModel ToDetails(ProductDto p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        ImageUrl = p.ImageUrl,
        StockQuantity = p.StockQuantity,
        CategoryName = p.CategoryName
    };
}
