using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models;

public class DistributorLevel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [Precision(18, 2)]
    public decimal MoneyNeeded { get; set; }
    
    [Precision(18, 2)]
    public decimal ImportReduction { get; set; }

    public int? NextLevelId { get; set; }
    public DistributorLevel? NextLevel { get; set; }

    public ICollection<Distributor> Distributors { get; set; } = new List<Distributor>();
}