using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Data.Models;

/// <summary>Категория продукти (напр. Букети, Рози, Саксийни цветя).</summary>
public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
