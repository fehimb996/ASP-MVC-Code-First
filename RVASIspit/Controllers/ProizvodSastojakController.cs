using RVASIspit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RVASIspit.Controllers
{
    public class ProizvodSastojakController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ActionResult Index()
        {
            var proizvodSastojci = db.SastojciProizvoda
                            .Include("Proizvod")
                            .Include("Sastojak")
                            .ToList();
            return View(proizvodSastojci);
        }

        public ActionResult Details(int? proizvodId, int? sastojakId)
        {
            if (proizvodId == null || sastojakId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProizvodSastojak proizvodSastojak = db.SastojciProizvoda.Find(proizvodId, sastojakId);
            if (proizvodSastojak == null)
            {
                return HttpNotFound();
            }
            return View(proizvodSastojak);
        }

        public ActionResult Create()
        {
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "Naziv");
            ViewBag.SastojakID = new SelectList(db.Sastojci, "SastojakID", "NazivSastojka");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProizvodID,SastojakID")] ProizvodSastojak proizvodSastojak)
        {
            if (ModelState.IsValid)
            {
                db.SastojciProizvoda.Add(proizvodSastojak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "Naziv", proizvodSastojak.ProizvodID);
            ViewBag.SastojakID = new SelectList(db.Sastojci, "SastojakID", "NazivSastojka", proizvodSastojak.SastojakID);
            return View(proizvodSastojak);
        }

        public ActionResult Delete(int? proizvodId, int? sastojakId)
        {
            if (proizvodId == null || sastojakId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProizvodSastojak proizvodSastojak = db.SastojciProizvoda.Find(proizvodId, sastojakId);
            if (proizvodSastojak == null)
            {
                return HttpNotFound();
            }
            return View(proizvodSastojak);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int proizvodId, int sastojakId)
        {
            ProizvodSastojak proizvodSastojak = db.SastojciProizvoda.Find(proizvodId, sastojakId);
            db.SastojciProizvoda.Remove(proizvodSastojak);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}