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

        // GET: Racuni
        public ActionResult Racuni()
        {
            var racuni = db.Racuni.Include(r => r.Klijent).Include(r => r.Zaposleni);
            return View(racuni.ToList());
        }

        // GET: Racun/Details/5
        public ActionResult Details(int? id)
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

        // GET: Racun/Create
        public ActionResult Create()
        {
            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime");
            ViewBag.ZaposleniID = new SelectList(db.Zaposleni, "ZaposleniID", "Ime");
            return View();
        }

        // POST: Racun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacunID,KlijentID,ZaposleniID,DatumIzdavanja,UkupnaCena")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                racun.DatumIzdavanja = DateTime.Now; // Postavljanje trenutnog vremena kao datuma izdavanja
                db.Racuni.Add(racun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime", racun.KlijentID);
            ViewBag.ZaposleniID = new SelectList(db.Zaposleni, "ZaposleniID", "Ime", racun.ZaposleniID);
            return View(racun);
        }

        // GET: Racun/Edit/5
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

        // POST: Racun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Racun/Delete/5
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

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuni.Find(id);
            db.Racuni.Remove(racun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}