using InvoiceTracking.Core.Entities;
using InvoiceTracking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTracking.Web.Pages;

public class TicketModel : PageModel
{
  private readonly ApplicationDbContext _context;

  public TicketModel(ApplicationDbContext context)
  {
    _context = context;
  }

  public Invoice? Invoice {get; set;}
  public PaymentTicket? Ticket {get; set;}

  public async Task<IActionResult> OnGetAsync(Guid id)
  {
    Invoice = await _context.Invoices
            .Include(i => i.PaymentTicket)
            .FirstOrDefaultAsync(i => i.Id == id);

    if (Invoice == null) return RedirectToPage("/Index");

    Ticket = Invoice.PaymentTicket ?? new PaymentTicket { InvoiceId = Invoice.Id, Invoice = Invoice };
    return Page();
  }

  public async Task<IActionResult> OnPostAsync(Guid Id, PaymentStatus Status, decimal? AmountPaid, DateTime? PaymentDate, string? PaymentMethod, string? ReferenceNumber, string Notes)
  {
    var invoice = await _context.Invoices
                .Include(i => i.PaymentTicket)
                .FirstOrDefaultAsync(i => i.Id == Id);
    
    if (invoice == null) return RedirectToPage("/Index");

    if (invoice.PaymentTicket == null)
    {
      invoice.PaymentTicket = new PaymentTicket { InvoiceId = Id };
      _context.PaymentTickets.Add(invoice.PaymentTicket);
    }

    var ticket = invoice.PaymentTicket;
    ticket.Status = Status;
    ticket.AmountPaid = AmountPaid;
    ticket.PaymentDate = PaymentDate;
    ticket.PaymentMethod = PaymentMethod;
    ticket.ReferenceNumber = ReferenceNumber;
    ticket.Notes = Notes;
    ticket.LastUpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return RedirectToPage("/Index");
  } 
}