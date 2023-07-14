namespace Behapy.Presentation.Models
{
    public class Promotions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Voucher { get; set; }
        public decimal Value { get; set; }
        public decimal MaxDiscount { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime StartAt { get; set; }
        public int TypeId { get; set; }

    }
}
