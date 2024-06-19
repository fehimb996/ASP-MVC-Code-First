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
    public class VrstaProizvodaController : Controller
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
            return View(db.VrsteProizvoda.ToList());
        }

        // Index i Details akcije su vidljive korisnicima u ulogama Admin i Korisnik
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VrstaProizvoda vrstaProizvoda = db.VrsteProizvoda.Find(id);
            if (vrstaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(vrstaProizvoda);
        }

        // Samo admin može pristupiti Create akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "VrstaProizvodaID,NazivVrste")] VrstaProizvoda vrstaProizvoda)
        {
            if (ModelState.IsValid)
            {
                db.VrsteProizvoda.Add(vrstaProizvoda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vrstaProizvoda);
        }

        // Samo admin može pristupiti Edit akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VrstaProizvoda vrstaProizvoda = db.VrsteProizvoda.Find(id);
            if (vrstaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(vrstaProizvoda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "VrstaProizvodaID,NazivVrste")] VrstaProizvoda vrstaProizvoda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vrstaProizvoda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vrstaProizvoda);
        }

        // Samo admin može pristupiti Delete akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VrstaProizvoda vrstaProizvoda = db.VrsteProizvoda.Find(id);
            if (vrstaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(vrstaProizvoda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            VrstaProizvoda vrstaProizvoda = db.VrsteProizvoda.Find(id);
            db.VrsteProizvoda.Remove(vrstaProizvoda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}