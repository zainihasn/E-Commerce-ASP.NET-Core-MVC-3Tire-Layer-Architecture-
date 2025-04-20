using AutoMapper;
using BusinessLogicLayer.Mapping;
using E_comm.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_comm.Extensions; // تأكد من إضافة namespace الخاص بالـ Extension Methods

var builder = WebApplication.CreateBuilder(args);

// تسجيلات منظمة
builder.Services.AddProjectDb(builder.Configuration);
builder.Services.AddProjectServices();
builder.Services.AddProjectIdentity();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// إعداد الجلسة
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// تكوين الـ HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
