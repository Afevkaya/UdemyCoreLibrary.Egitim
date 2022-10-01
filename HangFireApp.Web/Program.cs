using Hangfire;
using HangFireApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Kısaca Hangfire kütüphanesi işleyen iş akışını bozmadan arka planda yapılması gereken işlerin halledilmesi için geliştirilmiş kütüphanedir.
// Hangfire çalışma yapısı üç bölümden oluşmaktadır.
// - Client: Job ların(işlerin) oluşturulduğu yer.
// - Job Storage: Arka tarafta yürütülmesi gereken job ların depolandığı yer. db, inmemorycache
// - Hangfire Server: Storage da bulunan job ları belli aralıklara check eden ve işleyen yer.
// Client ile Hangfire server aynı yer olabilir.
// Arka tarafta ki işleri developerların takip edebilmesi için hangfire bir yapı sunmaktadır. Hangfire Dashboard.
// Hangfire Dashboard developer 'a güzel bir yapı sunmaktadır. Job ile alakalı her şey bulunmaktadır.
// Hangfire kütüphanesi genelde küçük projeler de kullanılması tavsiye edilir. Büyük projeler de bazı sıkıntılar doğurabilir.

// Hangfire kütüphanesini proje de kullanabilmek için önce projeye bazı paketlerin yüklenmesi gerekir.
// Ardından DI container 'a service olarak eklenmelidir.
// Eğer proje Hangfire Server olarak da çalışacaksa onu da DI container 'a service olarak eklenmesi gerekir.
// Hangfire 'a ait ayarlar appsettings.json dosyası altında bulunmakatadır.
// Proje ayağa kalktığında hanfire kütüphanesi kendi tablolarını kendi oluşturur.
// Program açılıp kapatıldığında eğer job lar bir db de tutuluyorsa kaldığı yerden devam eder.
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnection"));
});
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews();

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
// Hangfire dashboard 'u kullanabilmek için middleware olarak eklenmesi gerekir.
// middleware den patch'i belirlenebilir.
app.UseHangfireDashboard("/hangfire");
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();