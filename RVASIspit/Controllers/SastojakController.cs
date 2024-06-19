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
    public class SastojakController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [Authorize(Roles = "Admin, Korisnik")] // Samo registrovani korisnici sa ulogama Admin ili Korisnik mogu da pristupe Index
        public ActionResult Index()
        {
            var sastojci = db.Sastojci.ToList();
            return View(sastojci);
        }

        [Authorize(Roles = "Admin, Korisnik")] // Samo registrovani korisnici sa ulogama Admin ili Korisnik mogu da pristupe Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sastojak sastojak = db.Sastojci.Find(id);
            if (sastojak == null)
            {
                return HttpNotFound();
            }

            return View(sastojak);
        }

        [Authorize(Roles = "Admin")] // Samo admin može da pristupi Create formi
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Samo admin može da kreira novi sastojak
        public ActionResult Create([Bind(Include = "SastojakID,NazivSastojka")] Sastojak sastojak)
        {
            if (ModelState.IsValid)
            {
                db.Sastojci.Add(sastojak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sastojak);
        }

        [Authorize(Roles = "Admin")] // Samo admin može da pristupi Edit formi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sastojak sastojak = db.Sastojci.Find(id);
            if (sastojak == null)
            {
                return HttpNotFound();
            }

            return View(sastojak);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Samo admin može da izmeni sastojak
        public ActionResult Edit([Bind(Include = "SastojakID,NazivSastojka")] Sastojak sastojak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sastojak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sastojak);
        }

        [Authorize(Roles = "Admin")] // Samo admin može da pristupi Delete formi
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sastojak sastojak = db.Sastojci.Find(id);
            if (sastojak == null)
            {
                return HttpNotFound();
            }

            return View(sastojak);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Samo admin može da obriše sastojak
        public ActionResult DeleteConfirmed(int id)
        {
            Sastojak sastojak = db.Sastojci.Find(id);
            db.Sastojci.Remove(sastojak);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}