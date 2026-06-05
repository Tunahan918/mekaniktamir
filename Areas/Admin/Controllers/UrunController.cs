using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bakimonarim.Data;

namespace bakimonarim.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class UrunController : Controller
    {
        readonly AppDbContext db;
        readonly IWebHostEnvironment _env;

        public UrunController(AppDbContext dbContext, IWebHostEnvironment env)
        {
            db = dbContext;
            _env = env;
        }

        public IActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Kategori).OrderBy(u => u.Sira).ToList();
            return View(urunler);
        }

        public ActionResult Create()
        {
            ViewBag.Kategoriler = new SelectList(db.Kategoriler.ToList(), "KategorId", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("UrunId,Adi,Aciklama,Tarih,Onay,Sira,KategoriId")] Urun urun, IFormFile? resim)
        {
            ModelState.Remove("Kategori");
            ModelState.Remove("vitrinresim");

            if (resim != null && resim.Length > 0)
            {
                var klasor = Path.Combine(_env.WebRootPath, "img", "urunler");
                Directory.CreateDirectory(klasor);
                var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(resim.FileName);
                var yol = Path.Combine(klasor, dosyaAdi);
                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    await resim.CopyToAsync(stream);
                }
                urun.vitrinresim = "/img/urunler/" + dosyaAdi;
            }

            if (ModelState.IsValid)
            {
                db.Urunler.Add(urun);
                db.SaveChanges();
                TempData["Mesaj"] = "Ürün başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            ViewBag.Kategoriler = new SelectList(db.Kategoriler.ToList(), "KategorId", "Ad", urun.KategoriId);
            return View(urun);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            Urun? urun = db.Urunler.Find(id);
            if (urun == null) return NotFound();
            ViewBag.Kategoriler = new SelectList(db.Kategoriler.ToList(), "KategorId", "Ad", urun.KategoriId);
            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("UrunId,Adi,Aciklama,Tarih,Onay,Sira,KategoriId,vitrinresim")] Urun urun, IFormFile? resim)
        {
            ModelState.Remove("Kategori");
            ModelState.Remove("resim");

            if (resim != null && resim.Length > 0)
            {
                var klasor = Path.Combine(_env.WebRootPath, "img", "urunler");
                Directory.CreateDirectory(klasor);
                var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(resim.FileName);
                var yol = Path.Combine(klasor, dosyaAdi);
                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    await resim.CopyToAsync(stream);
                }
                urun.vitrinresim = "/img/urunler/" + dosyaAdi;
            }

            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Mesaj"] = "Ürün başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            ViewBag.Kategoriler = new SelectList(db.Kategoriler.ToList(), "KategorId", "Ad", urun.KategoriId);
            return View(urun);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            Urun? urun = db.Urunler.Include(u => u.Kategori).FirstOrDefault(u => u.UrunId == id);
            if (urun == null) return NotFound();
            return View(urun);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun? urun = db.Urunler.Find(id);
            if (urun != null)
            {
                db.Urunler.Remove(urun);
                db.SaveChanges();
            }
            TempData["Mesaj"] = "Ürün silindi.";
            return RedirectToAction("Index");
        }
    }
}
