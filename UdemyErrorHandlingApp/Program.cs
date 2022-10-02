var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Request --[DeveloperExceptionPage]--[UseExceptionHandler]--[UseStatusCodePages]---------------------------------------------------> Response

// DeveloperExceptionPage bir error handling örneğidir.
// Uygulama development aşamasındayken developer'a hata ile ilgili detaylı bilgi vermek için kullanılan bir middleware'dir
// Uygulama bazlı çalışır.

// UseExceptionHandler bir error handling örneğidir.
// Uygulama production aşamasındayken bir hata çıktığında kullanıcıya bir hata çıktığına dair bilgi vermek için kullanılan bir middlewaredir.
// Ugulama bazlı çalışır.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// UseStatusCodePages bir error handling örneğidir.
// Uygulamada çıkan hatanın durum kodunu ve mesajını dönen bir middlewaredir.
// Uygulama bazlıdır ve spesifik hale getirilebilir.

// app.UseStatusCodePages();
// app.UseStatusCodePages("text/plain","Bir hata var. Durum kodu: {0}");
// app.UseStatusCodePages(async context =>
// {
//     context.HttpContext.Response.ContentType = "text/plain";
//     await context.HttpContext.Response.WriteAsync(
//         $"Bir hata meydana geldi. Durum kodu: {context.HttpContext.Response.StatusCode}");
// });


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();