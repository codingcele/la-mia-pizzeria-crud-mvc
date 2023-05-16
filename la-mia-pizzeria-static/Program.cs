using la_mia_pizzeria_static;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<la_mia_pizzeria_static.PizzeriaContext>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<la_mia_pizzeria_static.PizzeriaContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICustomLogger, CustomConsoleLogger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pizzeria}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
