using System.ComponentModel.DataAnnotations;

namespace InvoiceTracking.Core.Entities;

public class Invoice {
  [Key]
  public Guid Id {get; set;} = Guid.NewGuid();

  public string InvoiceNumber {get; set;} = string.Empty;
  public string VendorName {get; set;} = string.Empty;
  public decimal TotalAmount {get; set;}
  public DateTime InvoiceDate {get; set;}
  public DateTime? DueDate {get; set;}

  //File storage information
  public string FileName {get; set;} = string.Empty;
  public string FilePath {get; set;} = string.Empty;
  public string ContentType {get; set;} = string.Empty;

  public DateTime UploadedAt {get; set;} = DateTime.UtcNow;
  public string UploadedBy {get; set;} = "System";

  // Navigation
  public PaymentTicket? PaymentTicket {get; set;}
}