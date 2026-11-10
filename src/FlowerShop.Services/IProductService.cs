using FlowerShop.Services.Models;

namespace FlowerShop.Services;

/// <summary>Бизнес логика за достъп до каталога с продукти.</summary>
public interface IProductService
{
    /// <summary>Връща наличните продукти, като по желание ги филтрира по категория и име.</summary>
    Task<IReadOnlyList<ProductDto>> GetAllAsync(int? categoryId = null, string? search = null);

    /// <summary>Връща най-новите налични продукти.</summary>
    Task<IReadOnlyList<ProductDto>> GetLatestAsync(int count);

    /// <summary>Връща продукт по идентификатор или null, ако не съществува.</summary>
    Task<ProductDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync();

    Task<int> CountAsync();
}
