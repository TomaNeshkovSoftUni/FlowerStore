namespace FlowerShop.Web.Models;

public class ProductDetailsViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public int StockQuantity { get; set; }

    public string CategoryName { get; set; } = null!;
}
