using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using bakimonarim.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/admin/login";
    option.AccessDeniedPath = "/admin/home/denied";
});

var app = builder.Build();

// Admin kullanıcı yoksa otomatik oluştur
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Yoneticiler.Any())
    {
        db.Yoneticiler.Add(new Yonetici
        {
            KullaniciAdi = "admin",
            Sifre = "123456",
            Gorevi = "admin",
            AdiSoyadi = "Yönetici"
        });
        db.SaveChanges();
    }
}
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
       name: "areas",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
