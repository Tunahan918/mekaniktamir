using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bakimonarim.Data;

namespace bakimonarim.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LoginController : Controller
    {
        readonly AppDbContext _dbContext;
        readonly IConfiguration _configuration;
        public LoginController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string eposta, string sifre, string returnUrl)
        {
            Yonetici? yonetici = await (from a in _dbContext.Yoneticiler where a.KullaniciAdi == eposta && a.Sifre == sifre select a).FirstOrDefaultAsync();
            if (yonetici != null)
            {
                var claims = new List<Claim>() {
                new Claim("adsoyad", yonetici.AdiSoyadi ?? ""),
                new Claim(ClaimTypes.Name, yonetici.YoneticiId.ToString()),
                new Claim(ClaimTypes.Role, yonetici.Gorevi ?? "user")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return returnUrl != null && returnUrl != "" ? Redirect(returnUrl) : Redirect("/admin/home");

            }
            TempData["error"] = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

    }
}