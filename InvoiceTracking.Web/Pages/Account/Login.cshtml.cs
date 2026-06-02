using InvoiceTracking.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceTracking.Web.Pages.Account;

public class LoginModel : PageModel
{
  private readonly SignInManager<ApplicationUser> _signInManager;

  public LoginModel(SignInManager<ApplicationUser> signInManager)
  {
    _signInManager = signInManager;
  }

  public async Task<IActionResult> OnPostAsync(string Email, string Password)
  {
    var result = await _signInManager.PasswordSignInAsync(Email, Password,isPersistent:false, lockoutOnFailure:false);
    if (result.Succeeded) return RedirectToPage("/Index");
    ModelState.AddModelError("","Invalid email or password.");
    return Page();
  }
}