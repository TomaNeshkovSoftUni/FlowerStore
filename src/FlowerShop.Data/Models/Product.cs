using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Data.Models;

/// <summary>Продукт от каталога - цвете, букет, саксия или подаръчен комплект.</summary>
public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = null!;

    /// <summary>Цена в евро.</summary>
    public decimal Price { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public int StockQuantity { get; set; }

    public bool IsAvailable { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
