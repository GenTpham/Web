using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Demo.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext") ?? throw new InvalidOperationException("Connection string 'DemoContext' not found.")));

builder.Services.AddControllersWithViews();

// Cấu hình xác thực
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
        options.LogoutPath = "/Users/Logout";
        options.AccessDeniedPath = "/Users/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Đặt thời gian hết hạn cho cookie
    });

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Đảm bảo UseAuthentication() được gọi trước UseAuthorization()
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();