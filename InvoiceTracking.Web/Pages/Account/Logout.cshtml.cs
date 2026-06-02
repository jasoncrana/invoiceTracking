using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceTracking.Web.Pages.Account;

public class LogoutModel : PageModel
{
  private readonly SignInManager<Core.Entities.ApplicationUser> _signInManager;

  public LogoutModel(SignInManager<Core.Entities.ApplicationUser> signInManager){
    _signInManager = signInManager;
  }

  public async Task<IActionResult> OnPostAsync()
  {
    await _signInManager.SignOutAsync();
    return RedirectToPage("/Account/Login");
  }
}