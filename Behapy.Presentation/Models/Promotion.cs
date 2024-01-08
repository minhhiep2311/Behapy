using System.ComponentModel.DataAnnotations;
using Behapy.Presentation.Enums;
using Behapy.Presentation.Extensions;
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

    [Display(Name = "Ẩn khỏi danh sách")]
    public bool IsHidden { get; set; }

    public List<Order> Orders { get; set; } = new();

    // public List<Category> Categories { get; set; } = new();
    public List<ProductPromotion> ProductPromotions { get; set; } = new();
    public List<OrderDetail> OrderDetails { get; set; } = new();

    public string Content
    {
        get
        {
            var typeName = Type switch
            {
                PromotionType.Order => "",
                PromotionType.Product => "trong danh sách",
                // PromotionType.Category => string.Join(", ", Categories.Select(c => c.Name)),
                _ => throw new ArgumentOutOfRangeException()
            };
            return
                $"{Name}: Giảm giá {Value}{Unit.GetName()} cho {Type.GetName(true)} {typeName} cho đơn hàng từ {(MinOrderValue ?? 0).ToMoney()}: <b>{Voucher}</b>";
        }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartAt >= EndAt)
            yield return new ValidationResult("Thời gian bắt đầu phải trước thời gian kết thúc",
                new List<string> { "StartAt" });
    }
}