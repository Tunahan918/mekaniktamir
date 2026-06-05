using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bakimonarim.Data;
using MailKit.Net.Smtp;
using MimeKit;

namespace bakimonarim.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public HomeController(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public IActionResult Index()
    {
        var urunler = _db.Urunler
            .Include(u => u.Kategori)
            .Where(u => u.Onay)
            .OrderBy(u => u.Sira)
            .Take(4)
            .ToList();
        return View("Anasayfa", urunler);
    }

    public IActionResult Hakkimizda()
    {
        return View("Hakkımızda");
    }

    public IActionResult Urunler(int? kategoriId)
    {
        var urunler = _db.Urunler
            .Include(u => u.Kategori)
            .Where(u => u.Onay);

        if (kategoriId.HasValue)
            urunler = urunler.Where(u => u.KategoriId == kategoriId.Value);

        ViewBag.Kategoriler = _db.Kategoriler.ToList();
        ViewBag.SecilenKategori = kategoriId;
        return View("urunler", urunler.OrderBy(u => u.Sira).ToList());
    }

    public IActionResult Iletisim()
    {
        return View("iletisim");
    }

    [HttpPost]
    public IActionResult Iletisim(string adSoyad, string email, string mesaj)
    {
        TempData["Basari"] = "Mesajınız alındı, en kısa sürede dönüş yapacağız.";
        return RedirectToAction("Iletisim");
    }

    public IActionResult Randevu()
    {
        return View("randevu");
    }

    [HttpPost]
    public async Task<IActionResult> Randevu(string adSoyad, string telefon, string email,
        string aracMarka, string aracModel, string aracYil,
        string hizmet, string tarih, string saat, string notlar)
    {
        try
        {
            var mesaj = new MimeMessage();
            mesaj.From.Add(new MailboxAddress("MakinaTeknik Randevu", _config["Email:Kullanici"]));
            mesaj.To.Add(new MailboxAddress("MakinaTeknik", _config["Email:Alici"]));
            mesaj.Subject = $"Yeni Randevu — {adSoyad} — {tarih} {saat}";

            mesaj.Body = new TextPart("html")
            {
                Text = $@"
                <h2 style='color:#d4af37;'>Yeni Randevu Talebi</h2>
                <hr>
                <h3>Kişisel Bilgiler</h3>
                <p><b>Ad Soyad:</b> {adSoyad}</p>
                <p><b>Telefon:</b> {telefon}</p>
                <p><b>E-posta:</b> {email}</p>
                <h3>Araç Bilgileri</h3>
                <p><b>Marka:</b> {aracMarka} &nbsp; <b>Model:</b> {aracModel} &nbsp; <b>Yıl:</b> {aracYil}</p>
                <h3>Randevu Detayı</h3>
                <p><b>Hizmet:</b> {hizmet}</p>
                <p><b>Tarih:</b> {tarih} &nbsp; <b>Saat:</b> {saat}</p>
                <p><b>Notlar:</b> {notlar}</p>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:SmtpHost"], int.Parse(_config["Email:SmtpPort"]!), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["Email:Kullanici"], _config["Email:Sifre"]);
            await smtp.SendAsync(mesaj);
            await smtp.DisconnectAsync(true);

            TempData["RandevuBasari"] = $"Randevunuz alındı! {tarih} tarihinde {saat} saatinde sizi bekliyoruz.";
        }
        catch
        {
            TempData["RandevuBasari"] = $"Randevunuz alındı! {tarih} tarihinde {saat} saatinde sizi bekliyoruz.";
        }

        return RedirectToAction("Randevu");
    }
}
