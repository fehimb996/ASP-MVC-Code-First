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

        public ActionResult Index()
        {
            var racuni = db.Racuni.Include(r => r.Klijent).Include(r => r.Zaposleni);
            return View(racuni.ToList());
        }

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

        public ActionResult Create()
        {
            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime");
            ViewBag.ZaposleniID = new SelectList(db.Zaposleni, "ZaposleniID", "Ime");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuni.Find(id);
            db.Racuni.Remove(racun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}