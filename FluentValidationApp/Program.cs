using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidationApp.FluentValidators;
using FluentValidationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Projeye hangi database ile çalýþtýðýmýzý belirtiyoruz.
// Bu servisi eklemz isek projenin hangi databse ile çalýþacaðýný bilemez.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConStr"]);
});

//builder.Services.AddSingleton<IValidator<Customer>, CustomerValidator>();

// Projeye FluentValidaton kütüphanesini kullanacaðýmýz haber etmemiz gerekli.
builder.Services.AddControllersWithViews().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<Program>();
});

// API tarafýndan gelen otomatik doðrulamayý bastýrmak için kullanýlýr.. 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
