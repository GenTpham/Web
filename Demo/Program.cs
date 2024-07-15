using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Demo.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext") ?? throw new InvalidOperationException("Connection string 'DemoContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.LoginPath = "/Users/Login";
         options.LogoutPath = "/Users/Logout";
         options.AccessDeniedPath = "/Users/AccessDenied";
         options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Đặt thời gian hết hạn cho cookie
     });


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
