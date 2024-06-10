using LosDragones.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnDB") ?? throw new InvalidOperationException("Connection string 'ConnDB' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<LosDragonesDBContext>(options => options
.UseSqlServer("name=ConnDB")
.LogTo(Console.WriteLine, LogLevel.Information));


builder.Services.AddDbContext<AuthContext>(options => options
.UseSqlServer("name=ConnDB")
.LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthContext>()
    .AddDefaultUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.MapRazorPages();

app.Run();
