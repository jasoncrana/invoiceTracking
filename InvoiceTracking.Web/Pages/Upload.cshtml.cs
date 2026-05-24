using InvoiceTracking.Core.Entities;
using InvoiceTracking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceTracking.Web.Pages;

public class UploadModel : PageModel{
  private readonly ApplicationDbContext _context;
  private readonly IWebHostEnvironment _environment;

  public UploadModel(ApplicationDbContext context, IWebHostEnvironment environment){
    _context = context;
    _environment = environment;
  }

  public void OnGet(){}

  public async Task<IActionResult> OnPostAsync(IFormFile invoiceFile, string InvoiceNumber, string VendorName, decimal TotalAmount, DateTime InvoiceDate){
    if (invoiceFile == null || invoiceFile.Length == 0){
      ModelState.AddModelError("","Please select a file.");
      return Page();
    }

    // create folder for uploads, if needed
    var uploadsFolder = Path.Combine(_environment.ContentRootPath, "wwwroot","uploads");
    Directory.CreateDirectory(uploadsFolder);

    var fileName = Guid.NewGuid() + Path.GetExtension(invoiceFile.FileName);
    var filePath = Path.Combine(uploadsFolder,fileName);

    await using var stream = new FileStream(filePath,FileMode.Create);
    await invoiceFile.CopyToAsync(stream);

    // create invoice from upload
    var invoice = new Invoice
    {
      InvoiceNumber = InvoiceNumber,
      VendorName = VendorName,
      TotalAmount = TotalAmount,
      InvoiceDate = InvoiceDate,
      FileName = invoiceFile.FileName,
      FilePath = "/uploads/"+fileName,
      ContentType = invoiceFile.ContentType
    };

    // create payment ticket from invoice
    var ticket = new PaymentTicket
    {
      Invoice = invoice
    };

    _context.Invoices.Add(invoice);
    await _context.SaveChangesAsync();

    return RedirectToPage("/Index");
  }


}