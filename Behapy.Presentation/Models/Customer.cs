using System.ComponentModel.DataAnnotations.Schema;
using Behapy.Presentation.Areas.Identity.Data;

namespace Behapy.Presentation.Models;

public class Customer
{
    public int Id { get; set; }
    public string? Address { get; set; }
    public DateTime? Birthday { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public List<Order> Orders { get; set; } = new();
    public List<CartItem> CartItems { get; set; } = new();

    [NotMapped]
    public string Name => User.FullName;
}
