using System.ComponentModel.DataAnnotations;

namespace Behapy.Presentation.Models;

public class Distributor
{
    public int Id { get; set; }

    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = null!;

    [Display(Name = "Vị trí")]
    public string Position { get; set; } = null!;

    public int DistributorLevelId { get; set; }
    public DistributorLevel DistributorLevel { get; set; } = null!;
}