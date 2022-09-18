using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// RateLimit kütüphanesinin uygulamda kullanılması için eklenmesi gereken bazı servisleri ekliyoruz. 
// appsettings.json içindeki key-value çiftlerini uygulamaya eklememizi sağlıyor.
builder.Services.AddOptions();
// Süreçte yapılan requestler neticesinde limit durum bilgilerini cache cache'de tutmak için memory cache'i aktifleştirmemiz gerekli.
builder.Services.AddMemoryCache();
// appsettings.json içinde bulunan RateLimit'e ait konfigürasyonları uygulamaya bildiriyoruz. Ip için
// builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

// appsettings.json içinde bulunan RateLimit'e ait konfigürasyonları uygulamaya bildiriyoruz. ClientId için
builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));

// appsettings.json içinde bulunan spesifik Ip kurallarını ve politikalarını uygulamaya bildiriyoruz. Ip için
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// appsettings.json içinde bulunan spesifik Ip kurallarını ve politikalarını uygulamaya bildiriyoruz. ClientId için
builder.Services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("ClientRateLimitPolicies"));

// Politika ve verilerin memory'de tutulacağını bildiriyoruz. 
// builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
// Gelen resquest deki dataları okuyabilmesi için uygulamaya bildiriyoruz.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// RateLimit için tüm yapılanmaları organize bil şekilde inşa edecek ana servisi ekliyoruz.
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// RateLimiti uygulamaya middleware olarak ekliyoruz. Ip için
// app.UseIpRateLimiting();

// RateLimiti uygulamaya middleware olarak ekliyoruz. ClientId için
app.UseClientRateLimiting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Policy içindeki kuralları kullanabilmek için aşağıdaki kodları kullanmamız gerekli.
// var ipPolicyStore = app.Services.GetRequiredService<IIpPolicyStore>(); 
// ipPolicyStore.SeedAsync().GetAwaiter().GetResult();

app.Run();