using System.ComponentModel.DataAnnotations;

namespace Behapy.Presentation.Models;

public class Distributor
{
    public int Id { get; set; }

    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = null!;

	[Display(Name = "Điện thoại")]
	public string Phone { get; set; } = null!;

	[Display(Name = "Tổng tiền mua hàng")]
	public decimal TotalMoney { get; set; }

	public int DistributorLevelId { get; set; }

    public DistributorLevel? DistributorLevel { get; set; }
}