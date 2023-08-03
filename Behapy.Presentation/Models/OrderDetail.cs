using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Precision(18, 2)]
        public decimal Discount { get; set; }

        public int Amount { get; set; }
        public int OrderNumber { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
