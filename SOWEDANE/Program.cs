using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOWEDANE.EntityFrameworkContext;
using SOWEDANE.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var DbConnection=builder.Configuration.GetConnectionString("DatabaseConnection");
//builder.Services.AddDbContext<DbContext>(optionsAction :(options) => 
//{ options.UseSqlServer(DbConnection); });

//builder.Services.AddDbContext<UserContext>(optionsAction: (options) =>
//{ options.UseSqlServer(DbConnection); });

builder.Services.AddDbContext<ApplicationDbContext>(optionsAction: (options) =>
{ options.UseSqlServer(DbConnection); });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<UserOtpDbContext>(optionsAction: (options) =>
{ options.UseSqlServer(DbConnection); });

builder.Services.AddScoped<UserOtpController>();

//builder.Services.AddScoped<UserContext>();

var app = builder.Build();

app.UseSession();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Welcome}/{id?}");

app.Run();
