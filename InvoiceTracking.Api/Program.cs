using InvoiceTracking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add services to container
builder.Services.AddControllers();

// register DBContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options => 
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();