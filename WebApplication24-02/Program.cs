using Microsoft.EntityFrameworkCore;
using WebApplication24_02.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for Razor Pages and MVC controllers
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Register DbContext for DI using connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=SampleDb;Trusted_Connection=True;TrustServerCertificate=True;";
builder.Services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{           
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map controller routes and Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
