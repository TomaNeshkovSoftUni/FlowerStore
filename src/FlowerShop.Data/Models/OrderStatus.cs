namespace FlowerShop.Data.Models;

/// <summary>Състояния на една поръчка.</summary>
public enum OrderStatus
{
    Pending = 0,     // Получена
    Confirmed = 1,   // Потвърдена
    Preparing = 2,   // Приготвя се
    Delivering = 3,  // Доставя се
    Delivered = 4,   // Доставена
    Cancelled = 5    // Отказана
}
