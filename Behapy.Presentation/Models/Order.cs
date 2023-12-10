using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models;

public class Order
{
    public int Id { get; set; }

    [Display(Name = "Giảm giá")]
    [Precision(18, 2)]
    public decimal Discount { get; set; }

    [Display(Name = "Tổng tiền")]
    [Precision(18, 2)]
    public decimal TotalMoney { get; set; }

    [Display(Name = "Thời gian hủy")]
    public DateTime? CancelledAt { get; set; }

    [Display(Name = "Thời gian tạo")]
    public DateTime CreateAt { get; set; }

    [Display(Name = "Ghi chú")]
    public string Note { get; set; } = null!;

    [Display(Name = "Trạng thái")]
    public string CurrentStatus { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = null!;

    public int PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; set; } = null!;

    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int? DistributorId { get; set; }
    public Distributor? Distributor { get; set; }

    public int? PromotionId { get; set; }
    public Promotion? Promotion { get; set; }

    public List<OrderStatus> OrderStatuses { get; set; } = new();
    public List<OrderDetail> OrderDetails { get; set; } = new();
}