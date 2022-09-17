using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidationApp.FluentValidators;
using FluentValidationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AutoMapper kütüphanesini projede kullanabilmek için projeye servis olarak eklemek gerekir.
builder.Services.AddAutoMapper(typeof(Program));

// Projeye hangi database ile �al��t���m�z� belirtiyoruz.
// Bu servisi eklemz isek projenin hangi databse ile �al��aca��n� bilemez.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConStr"]);
});

//builder.Services.AddSingleton<IValidator<Customer>, CustomerValidator>();

// Projeye FluentValidaton k�t�phanesini kullanaca��m�z haber etmemiz gerekli.
builder.Services.AddControllersWithViews().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<Program>();
});

// API taraf�ndan gelen otomatik do�rulamay� bast�rmak i�in kullan�l�r.. 
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
