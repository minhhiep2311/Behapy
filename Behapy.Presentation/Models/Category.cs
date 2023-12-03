using System.ComponentModel.DataAnnotations;

namespace Behapy.Presentation.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public string Name { get; set; } = null!;

        [Display(Name = "Mô tả")]
        public string Description { get; set; } = null!;

        public List<Product> Products { get; set; } = new();
    }
}
