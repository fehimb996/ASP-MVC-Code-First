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
    public class StavkaRacunaController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Index()
        {
            var stavkeRacuna = db.StavkeRacuna.Include(s => s.Proizvod).Include(s => s.Racun);
            return View(stavkeRacuna.ToList());
        }

        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Details(int? racunID, int? proizvodID)
        {
            if (racunID == null || proizvodID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Include(s => s.Proizvod).Include(s => s.Racun)
                                                       .FirstOrDefault(s => s.RacunID == racunID && s.ProizvodID == proizvodID);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "Naziv");
            ViewBag.RacunID = new SelectList(db.Racuni, "RacunID", "RacunID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "RacunID,ProizvodID,Cena")] StavkaRacuna stavkaRacuna)
        {
            if (ModelState.IsValid)
            {
                db.StavkeRacuna.Add(stavkaRacuna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "Naziv", stavkaRacuna.ProizvodID);
            ViewBag.RacunID = new SelectList(db.Racuni, "RacunID", "RacunID", stavkaRacuna.RacunID);
            return View(stavkaRacuna);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? racunID, int? proizvodID)
        {
            if (racunID == null || proizvodID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(racunID, proizvodID);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "Naziv", stavkaRacuna.ProizvodID);
            ViewBag.RacunID = new SelectList(db.Racuni, "RacunID", "RacunID", stavkaRacuna.RacunID);

            return View(stavkaRacuna);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "RacunID,ProizvodID,Cena")] StavkaRacuna stavkaRacuna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stavkaRacuna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "Naziv", stavkaRacuna.ProizvodID);
            ViewBag.RacunID = new SelectList(db.Racuni, "RacunID", "RacunID", stavkaRacuna.RacunID);

            return View(stavkaRacuna);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? racunID, int? proizvodID)
        {
            if (racunID == null || proizvodID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(racunID, proizvodID);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int racunID, int proizvodID)
        {
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(racunID, proizvodID);
            db.StavkeRacuna.Remove(stavkaRacuna);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}