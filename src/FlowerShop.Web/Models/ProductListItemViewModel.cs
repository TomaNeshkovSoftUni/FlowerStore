namespace FlowerShop.Web.Models;

public class ProductListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public string CategoryName { get; set; } = null!;
}
