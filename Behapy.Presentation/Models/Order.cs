namespace Behapy.Presentation.Models
{
    public class Order
    {
        public decimal Discount { get; set; }
        public int PaymentTypeId { get; set;}
        public int PromotionId { get; set;}
        public decimal TotalMoney { get; set;}
        public DateTime CancelledAt { get; set; }
        public DateTime CreateAt { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public string Note { get; set; }
        public string CurrentStatus { get; set; }
    }
}
