using System.ComponentModel.DataAnnotations;
using Behapy.Presentation.Enums;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models;

[Index(nameof(Voucher), IsUnique = true)]
public class Promotion : IValidatableObject
{
    public int Id { get; init; }

    [Display(Name = "Tên chương trình")]
    public string Name { get; set; } = null!;

    [Display(Name = "Giá trị")]
    [Precision(18, 2)]
    public decimal Value { get; set; }

    [Display(Name = "Đơn vị")]
    public PromotionUnit Unit { get; set; }

    [Display(Name = "Mã voucher")]
    public string Voucher { get; set; } = null!;

    [Display(Name = "Giảm giá tối đa")]
    [Precision(18, 2)]
    public decimal? MaxDiscount { get; set; }

    [Display(Name = "Giá trị đơn tối thiểu")]
    [Precision(18, 2)]
    public decimal? MinOrderValue { get; set; }

    [Display(Name = "Ngày bắt đầu")]
    public DateTime StartAt { get; set; }

    [Display(Name = "Ngày kết thúc")]
    public DateTime EndAt { get; set; }

    [Display(Name = "Loại giảm giá")]
    public PromotionType Type { get; set; }

    public List<Order> Orders { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<OrderDetail> OrderDetails { get; set; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartAt >= EndAt)
            yield return new ValidationResult("Thời gian bắt đầu phải trước thời gian kết thúc",
                new List<string> { "StartAt" });
    }
}