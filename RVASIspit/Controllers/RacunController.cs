using RVASIspit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RVASIspit.Controllers
{
    public class RacunController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // Index i Details akcije su vidljive korisnicima u ulogama Admin i Korisnik
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Index()
        {
            var racuni = db.Racuni.Include(r => r.Klijent).Include(r => r.Zaposleni);
            return View(racuni.ToList());
        }

        // Index i Details akcije su vidljive korisnicima u ulogama Admin i Korisnik
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Uključujemo učitavanje Klijenta i Zaposlenog za specifičan Racun
            Racun racun = db.Racuni.Include(r => r.Klijent).Include(r => r.Zaposleni).FirstOrDefault(r => r.RacunID == id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // Samo admin može pristupiti Create akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime");
            ViewBag.ZaposleniID = new SelectList(db.Zaposleni, "ZaposleniID", "Ime");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "RacunID,KlijentID,ZaposleniID,DatumIzdavanja,UkupnaCena")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                racun.DatumIzdavanja = DateTime.Now;
                db.Racuni.Add(racun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime", racun.KlijentID);
            ViewBag.ZaposleniID = new SelectList(db.Zaposleni, "ZaposleniID", "Ime", racun.ZaposleniID);
            return View(racun);
        }

        // Samo admin može pristupiti Edit akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime", racun.KlijentID);
            ViewBag.ZaposleniID = new SelectList(db.Zaposleni, "ZaposleniID", "Ime", racun.ZaposleniID);
            return View(racun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "RacunID,KlijentID,ZaposleniID,DatumIzdavanja,UkupnaCena")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime", racun.KlijentID);
            ViewBag.ZaposleniID = new SelectList(db.Zaposleni, "ZaposleniID", "Ime", racun.ZaposleniID);
            return View(racun);
        }

        // Samo admin može pristupiti Delete akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuni.Find(id);
            db.Racuni.Remove(racun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}