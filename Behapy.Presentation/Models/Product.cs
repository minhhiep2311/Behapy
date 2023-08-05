using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Behapy.Presentation.Models;

public class Product
{
	public int Id { get; set; }

	[Display(Name = "Tên sản phẩm")]
	public string Name { get; set; } = null!;

	[Precision(18, 2)]
	[Display(Name = "Giá tiền")]
	public decimal Price { get; set; }

	[Display(Name = "Kích hoạt")]
	public bool IsActive { get; set; }
	
	[Display(Name = "Mô tả")]
	public string Description { get; set; } = null!;
	
	[Display(Name = "Ảnh")]
	public string ImageUrl { get; set; } = null!;

	[Precision(18, 2)]
	[Display(Name = "Giảm giá")]
	public decimal Discount { get; set; }

	[Display(Name = "Thời gian tạo")]
	public DateTime CreatedAt { get; set; }

	[Display(Name = "Danh mục")]
	public int CategoryId { get; set; }
	public Category? Category { get; set; }

	[Display(Name = "Khuyến mãi")]
	public int? PromotionId { get; set; }
	public Promotion? Promotion { get; set; }

	public List<OrderDetail> OrderDetails { get; set; } = new();
	public List<CartItem> CartItems { get; set; } = new();
}
