namespace Behapy.Presentation.Models;

public class PaymentType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<Order> Orders { get; set; } = new();
}