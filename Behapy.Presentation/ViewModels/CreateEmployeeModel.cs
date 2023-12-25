using System.ComponentModel.DataAnnotations;

namespace Behapy.Presentation.ViewModels;

public class CreateEmployeeModel
{
    public int Id { get; set; }

    [Display(Name = "Họ tên")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = null!;

    [Display(Name = "Vị trí")]
    public string Position { get; set; } = null!;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Số điện thoại")]
    public string PhoneNumber { get; set; } = null!;
}