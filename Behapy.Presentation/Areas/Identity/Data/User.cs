using Microsoft.AspNetCore.Identity;

namespace Behapy.Presentation.Areas.Identity.Data;

public class User : IdentityUser<string>
{
    public DateTime CreatedAt { get; set; }
    public string? ImageUrl { get; set; }
}
