namespace Behapy.Presentation.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderId { get; set; }

    }
}
