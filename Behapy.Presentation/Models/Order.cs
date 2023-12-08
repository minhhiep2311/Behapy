using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models;

public class Order
{
    public int Id { get; set; }

    [Precision(18, 2)]
    public decimal Discount { get; set; }

    [Precision(18, 2)]
    public decimal TotalMoney { get; set; }

    public DateTime? CancelledAt { get; set; }
    public DateTime CreateAt { get; set; }

    [Display(Name = "Ghi chú")]
    public string Note { get; set; } = null!;

    public string CurrentStatus { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = null!;

    [Display(Name = "Hình thức thanh toán")]
    public int PaymentTypeId { get; set; }

    public PaymentType PaymentType { get; set; } = null!;

    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int? DistributorId { get; set; }
    public Distributor? Distributor { get; set; }

    public int? PromotionId { get; set; }
    public Promotion? Promotion { get; set; }

    public List<OrderStatus> OrderStatuses { get; set; } = new();
    public IEnumerable<OrderDetail> OrderDetails { get; set; } = Enumerable.Empty<OrderDetail>();
}