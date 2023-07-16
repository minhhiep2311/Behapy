namespace Behapy.Presentation.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public int Amount { get; set; }
        public int OrderNumber { get; set; }
        public int OrderId { get; set; }
        public int PromotionId { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }

    }
}
