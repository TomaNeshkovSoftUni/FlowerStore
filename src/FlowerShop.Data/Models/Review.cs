using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Data.Models;

/// <summary>Отзив и оценка на продукт от потребител.</summary>
public class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
