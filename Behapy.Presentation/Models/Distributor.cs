namespace Behapy.Presentation.Models;

public class Distributor
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int DistributorLevelId { get; set; }
    public DistributorLevel DistributorLevel { get; set; } = null!;
}