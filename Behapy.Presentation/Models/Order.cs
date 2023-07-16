using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Precision(18, 2)]
        public decimal Discount { get; set; }

        [Precision(18, 2)]
        public decimal TotalMoney { get; set; }
        public DateTime CancelledAt { get; set; }
        public DateTime CreateAt { get; set; }
        public string Note { get; set; } = null!;
        public string CurrentStatus { get; set; } = null!;

        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        public List<OrderStatus> OrderStatuses { get; set; } = null!;
        public List<OrderDetail> OrderDetails { get; set; } = null!;
    }
}
