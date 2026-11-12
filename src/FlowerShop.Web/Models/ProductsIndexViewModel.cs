namespace FlowerShop.Web.Models;

public class ProductsIndexViewModel
{
    public IReadOnlyList<ProductListItemViewModel> Products { get; set; } = Array.Empty<ProductListItemViewModel>();

    public IReadOnlyList<CategoryViewModel> Categories { get; set; } = Array.Empty<CategoryViewModel>();

    public int? SelectedCategoryId { get; set; }

    public string? Search { get; set; }
}
