using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Behapy.Presentation.Areas.Identity.Data;

public class User : IdentityUser<string>
{
    [Display(Name = "Họ tên")]
    public string FullName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ImageUrl { get; set; }
}