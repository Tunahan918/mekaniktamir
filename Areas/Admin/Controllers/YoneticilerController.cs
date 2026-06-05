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
    [Authorize]
    public class YoneticilerController : Controller
    {
        readonly AppDbContext db;
        readonly IConfiguration _configuration;
        public YoneticilerController(AppDbContext dbContext, IConfiguration configuration)
        {
            db = dbContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(db.Yoneticiler.ToList());
        }
        
        // GET: Admin/Yonetici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Yonetici yonetici = db.Yoneticiler.Find(id);
            if (yonetici == null)
            {
                return NotFound();
            }
            return View(yonetici);
        }

        // GET: Admin/Yonetici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Yonetici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("YoneticiId,KullaniciAdi,Sifre,Gorevi,AdiSoyadi")] Yonetici yonetici)
        {
            if (ModelState.IsValid)
            {
                db.Yoneticiler.Add(yonetici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yonetici);
        }

        // GET: Admin/Yonetici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Yonetici yonetici = db.Yoneticiler.Find(id);
            if (yonetici == null)
            {
                return NotFound();
            }
            return View(yonetici);
        }

        // POST: Admin/Yonetici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("YoneticiId,KullaniciAdi,Sifre,Gorevi,AdiSoyadi")] Yonetici yonetici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yonetici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yonetici);
        }

        // GET: Admin/Yonetici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Yonetici yonetici = db.Yoneticiler.Find(id);
            if (yonetici == null)
            {
                return NotFound();
            }
            return View(yonetici);
        }

        // POST: Admin/Yonetici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yonetici yonetici = db.Yoneticiler.Find(id);
            db.Yoneticiler.Remove(yonetici);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}