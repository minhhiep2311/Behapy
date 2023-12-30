using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public string? Note { get; set; }

    [Display(Name = "Trạng thái")]
    public string CurrentStatus { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = null!;

    [Display(Name = "Phương thức thanh toán")]
    public int PaymentTypeId { get; set; }

    public PaymentType? PaymentType { get; set; }

    [Display(Name = "Khách hàng")]
    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }

    [Display(Name = "Nhà phân phối")]
    public int? DistributorId { get; set; }

    public Distributor? Distributor { get; set; }

    [Display(Name = "Mã giảm giá")]
    public int? PromotionId { get; set; }

    public Promotion? Promotion { get; set; }

    public List<OrderStatus> OrderStatuses { get; set; } = new();
    public List<OrderDetail> OrderDetails { get; set; } = new();

    [NotMapped]
    public bool IsCustomer { get; set; }

    [NotMapped]
    public List<CreateOrderProduct> Products { get; set; } = new();
}

public class CreateOrderProduct
{
    public int Id { get; set; }
    public int Amount { get; set; }
}