using System.ComponentModel.DataAnnotations;

namespace InvoiceTracking.Core.Entities;

public class PaymentTicket{
  [Key]
  public Guid Id {get; set;} = Guid.NewGuid();

  public Guid InvoiceId {get; set;} 
  public Invoice Invoice {get; set;} = null!;

  public PaymentStatus Status {get; set;} = PaymentStatus.Unpaid;

  public decimal? AmountPaid{get; set;}
  public DateTime? PaymentDate {get; set;}
  public string? PaymentMethod {get; set;}
  public string? ReferenceNumber {get; set;}

  public string? Notes {get; set;}

  public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
  public DateTime? LastUpdatedAt {get;set;}
}

public enum PaymentStatus{
  Unpaid,
  PartiallyPaid,
  Paid,
  Overdue,
  Cancelled
}