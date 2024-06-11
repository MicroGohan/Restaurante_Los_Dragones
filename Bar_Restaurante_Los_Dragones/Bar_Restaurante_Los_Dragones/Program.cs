
using Bar_Restaurante_Los_Dragones.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("ConnDB") ?? throw new InvalidOperationException("Connection string 'ConnDB' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<LosDragonesDBContext>(options => options.UseSqlServer("name=ConnDB"));

builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer("name=ConnDB"));



builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // Ruta de inicio de sesi�n
    options.LogoutPath = "/Identity/Account/Logout"; // Ruta de cierre de sesi�n
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Ruta de acceso denegado
});

builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated && !context.Request.Path.StartsWithSegments("/Identity/Account/Login"))
    {
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");



app.MapRazorPages();

app.Run();
