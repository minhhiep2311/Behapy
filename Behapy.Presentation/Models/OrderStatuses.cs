namespace Behapy.Presentation.Models
{
    public class OrderStatuses
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderId { get; set; }

    }
}
