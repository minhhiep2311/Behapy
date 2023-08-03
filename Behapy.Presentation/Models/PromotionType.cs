namespace Behapy.Presentation.Models
{
    public class PromotionType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<Promotion> Promotions { get; set; } = new();
    }
}
