using InvoiceTracking.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTracking.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

  public DbSet<Invoice> Invoices {get; set;}
  public DbSet<PaymentTicket> PaymentTickets {get; set;}

  protected override void OnModelCreating(ModelBuilder modelBuilder){
    base.OnModelCreating(modelBuilder);

    // one-to-one relationship between Invoice and PaymentTicket.
    // Confirmed with Finance Committee member responsible for making payments.
    modelBuilder.Entity<Invoice>()
                .HasOne(i => i.PaymentTicket)
                .WithOne(t => t.Invoice)
                .HasForeignKey<PaymentTicket>(t => t.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
  }
}