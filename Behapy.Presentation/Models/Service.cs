namespace Behapy.Presentation.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public int CategroyId { get; set; }
        public ServiceCategory Category { get; set; } = null!;
    }
}
