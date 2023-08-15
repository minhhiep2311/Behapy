using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models;

public class Promotion
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;

    [Precision(18, 2)]
    public decimal Value { get; set; }
    public string Unit { get; set; } = null!;
    public string? Voucher { get; set; }

    [Precision(18, 2)]
    public decimal? MaxDiscount { get; set; }
    public DateTime EndAt { get; set; }
    public DateTime StartAt { get; set; }

    public int TypeId { get; set; }
    public PromotionType Type { get; set; } = new();

    public List<Order> Orders { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<OrderDetail> OrderDetails { get; set; } = new();
}