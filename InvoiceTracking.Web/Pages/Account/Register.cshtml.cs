using InvoiceTracking.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceTracking.Web.Pages.Account;

public class RegisterModel : PageModel
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;

  public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
  {
    _userManager = userManager;
    _signInManager = signInManager;
  }

  public async Task<IActionResult> OnPostAsync(string FullName, string Email, string Password)
  {
    var user = new ApplicationUser
    {
      UserName = Email,
      Email = Email,
      FullName = FullName
    };

    var result = await _userManager.CreateAsync(user, Password);

    if (result.Succeeded)
    {
      await _signInManager.SignInAsync(user, isPersistent:false);
      return RedirectToPage("/Index");
    }

    foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);

    return Page();

  }
}