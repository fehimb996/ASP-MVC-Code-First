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

        public ActionResult Index()
        {
            var stavkeRacuna = db.StavkeRacuna.Include(s => s.Proizvod).Include(s => s.Racun);
            return View(stavkeRacuna.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }

        public ActionResult Create()
        {
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "Naziv");
            ViewBag.RacunID = new SelectList(db.Racuni, "RacunID", "RacunID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StavkaRacunaID,RacunID,ProizvodID,Cena")] StavkaRacuna stavkaRacuna)
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
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
        public ActionResult Edit([Bind(Include = "StavkaRacunaID,RacunID,ProizvodID,Cena")] StavkaRacuna stavkaRacuna)
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
            db.StavkeRacuna.Remove(stavkaRacuna);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}