namespace Behapy.Presentation.ViewModels;

public record ProductAmount(int Id, string Name, int Amount);

public class CartUpdateViewModel
{
    public List<ProductAmount> Products { get; set; } = new();
    public string? Voucher { get; set; }
}