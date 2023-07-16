using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Discount { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}
