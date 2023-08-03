namespace Behapy.Presentation.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
