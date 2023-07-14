namespace Behapy.Presentation.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SerCatId { get; set; }
    }
}
