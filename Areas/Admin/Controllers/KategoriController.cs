using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bakimonarim.Data;

namespace bakimonarim.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class KategoriController : Controller
    {
        readonly AppDbContext db;

        public KategoriController(AppDbContext dbContext)
        {
            db = dbContext;
        }

        public IActionResult Index()
        {
            return View(db.Kategoriler.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            Kategori? kategori = db.Kategoriler.Find(id);
            if (kategori == null) return NotFound();
            return View(kategori);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("KategorId,Ad,Seo,Sira")] Kategori kategori)
        {
            kategori.Urunler = new List<Urun>();
            ModelState.Remove("Urunler");
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            Kategori? kategori = db.Kategoriler.Find(id);
            if (kategori == null) return NotFound();
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("KategorId,Ad,Seo,Sira")] Kategori kategori)
        {
            kategori.Urunler = new List<Urun>();
            ModelState.Remove("Urunler");
            if (ModelState.IsValid)
            {
                db.Entry(kategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            Kategori? kategori = db.Kategoriler.Find(id);
            if (kategori == null) return NotFound();
            return View(kategori);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori? kategori = db.Kategoriler.Find(id);
            if (kategori != null)
            {
                db.Kategoriler.Remove(kategori);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
