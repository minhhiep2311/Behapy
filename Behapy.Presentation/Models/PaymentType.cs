using System.ComponentModel.DataAnnotations;

namespace Behapy.Presentation.Models;

public class PaymentType
{
    public int Id { get; set; }
    
    [Display(Name = "Hình thức thanh toán")]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public List<Order> Orders { get; set; } = new();
}