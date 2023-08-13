using Behapy.Presentation.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Behapy.Presentation.Controllers;

public class IdentityController : Controller
{
    private readonly SignInManager<User> _signInManager;

    public IdentityController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    // POST /Identity/Login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe)
    {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToPage("~/");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return RedirectToPage("/Account/Login");
    }
}