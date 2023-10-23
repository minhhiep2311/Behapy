using System.ComponentModel.DataAnnotations;

namespace Behapy.Presentation.ViewModels;

public class CheckoutViewModel
{
    [Display(Name = "Họ tên")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [Display(Name = "Hình thức thanh toán")]
    public int PaymentTypeId { get; set; }

    [Display(Name = "Giao tới địa chỉ khác?")]
    public bool UseAnotherAddress { get; set; }

    [Display(Name = "Địa chỉ")]
    public string? AnotherAddress { get; set; }

    [Display(Name = "Ghi chú")]
    public string? Note { get; set; }
}