using Smidge;
using Smidge.Cache;
using Smidge.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Smidge kütüphanesi js ve/veya css dosyalarını bundle ve minification etmemizi sağlar. Peki ya nedir bu terimler?
// Bundle js ve/veya css dosyalarını birleştirme işlemine denir.
// Minification js ve/veya css dosyalarında gereksiz bulunan yorum satırlarını, boşlukları kaldırma işlemine denir.
// Bu sayede dosyalar tarayıcıya yani kullancıya gösterilirken upload(yüklenme) süresini kısaltmayı sağlar.

// Smidge kütüphanesini proje de kullanabilmek için önce pakedi projeye eklenmesi gerekir.
// Daha sonra bunu service olarak Program.cs classına eklenmesi gerekir.
// Proje ayağa kalkarken hangi servislerin kullanacağını DI Containera ekler.
builder.Services.AddSmidge(builder.Configuration.GetSection("smidge"));
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

app.UseRouting();

app.UseAuthorization();
// Smidge kütüphanesini projede kullanabilme için middleware olarak eklenmesi gerekir.
app.UseSmidge(bundle =>
{
    // Middleware içinde birleştirilmek istenen js ve css dosyaları belirtilir.
    // İstenilen yerde verilen isimle kullanılır.
    // appsettings.js dosyası içinde smidge kütüphaneisne ait ayarlar içinde cachelenecek klasör isminde proje ayağa kaltığında dosya oluşur.
    // Birleştirilecek dosyalar aynı klasör altında ise sadece klasör ismi verilerek de dosyalar birleştirilebilir.
    // Bazen spesifik ihtiyaçlar için ortamlara göre bundle ve min işlemlerini özelleştirmek gerekebilir.
    // Aşağıdaki kodda debug modda bazı özelleştirilmeler yapılmıştır.
    
    // bundle.CreateJs("my-js-bundle", "~/js/site.js", "~/js/site2.js");
    // bundle.CreateJs("my-js-bundle", "~/js/");
    bundle.CreateJs("my-js-bundle", "~/js/").WithEnvironmentOptions(BundleEnvironmentOptions.Create().ForDebug(
        build =>
        {
            build.EnableCompositeProcessing()
                .EnableCompositeProcessing()
                .SetCacheBusterType<AppDomainLifetimeCacheBuster>()
                .CacheControlOptions(enableEtag: false, cacheControlMaxAge: 0);
        }).Build());
    bundle.CreateCss("my-css-bundle", "~/css/site.css", "~/lib/bootstrap/dist/css/bootstrap.css");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();