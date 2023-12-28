using System.ComponentModel.DataAnnotations;
using Behapy.Presentation.Models;

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

    public static CreateEmployeeModel FromEmployee(Employee employee)
    {
        return new CreateEmployeeModel
        {
            Id = employee.Id,
            FullName = employee.FullName,
            Address = employee.Address,
            Position = employee.Position,
            Email = employee.User?.Email ?? string.Empty,
            PhoneNumber = employee.User?.PhoneNumber ?? string.Empty,
        };
    }

    public Employee ToEmployee()
    {
        return new Employee
        {
            Id = Id,
            FullName = FullName,
            Address = Address,
            Position = Position,
        };
    }
}