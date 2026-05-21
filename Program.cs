using Microsoft.EntityFrameworkCore;
using PerdeCim.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Servisleri Kaydet
builder.Services.AddControllersWithViews();

// Veritabanı Bağlantısı (SQLite)
builder.Services.AddDbContext<DataContext>(options =>
   options.UseSqlite(builder.Configuration.GetConnectionString("baglanti")));

// Session (Oturum) Servisini Yapılandır
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 2. HTTP Pipeline (Ara Yazılımlar)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseRouting();

// --- KRİTİK SIRALAMA ---
// UseSession, UseRouting'den sonra gelmelidir.
app.UseSession(); 
app.UseAuthorization();
// -----------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();