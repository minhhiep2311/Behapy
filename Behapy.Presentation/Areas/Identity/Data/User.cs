using Microsoft.AspNetCore.Identity;

namespace Behapy.Presentation.Areas.Identity.Data;

public class User : IdentityUser<string>
{
    public string FullName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ImageUrl { get; set; }
}