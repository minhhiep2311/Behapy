namespace Behapy.Presentation.Models
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<Service> Services { get; set; } = new();
    }
}
