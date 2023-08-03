namespace Behapy.Presentation.Models;

public class CartItem
{
	public int Id { get; set; }
	public int Amount { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	public int CustomerId { get; set; }
	public Customer Customer { get; set; } = null!;

	public int ProductId { get; set; }
	public Product Product { get; set; } = null!;
}

