using InvoiceTracking.Core.Entities;
using InvoiceTracking.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTracking.Web.Pages;

[Authorize]
public class IndexModel : PageModel
{
  private readonly ApplicationDbContext _context;

  public IndexModel(ApplicationDbContext context)
  {
    _context = context;
  }

  public List<Invoice> Invoices { get; set; } = new();

  public async Task OnGetAsync()
  {
    Invoices = await _context.Invoices
             .Include(i => i.PaymentTicket)
             .OrderByDescending(i => i.UploadedAt)
             .ToListAsync();
  }

  // public string GetStatusBadge(PaymentStatus? status)
  // {
  //   return status switch
  //   {
  //     PaymentStatus.Paid => "bg-success",
  //     PaymentStatus.PartiallyPaid => "bg-warning",
  //     PaymentStatus.Overdue => "bg-danger",
  //     _ => "bg-secondary" // default
  //   };
  // }
}
