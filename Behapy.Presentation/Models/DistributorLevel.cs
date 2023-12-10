using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Models;

public class DistributorLevel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [Precision(18, 2)]
    public decimal MoneyNeeded { get; set; }

    public int? NextLevel { get; set; }

    public IEnumerable<Distributor> Distributors { get; set; } = Enumerable.Empty<Distributor>();
}