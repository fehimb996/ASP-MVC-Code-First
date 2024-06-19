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
    public class GrupaProizvodaController : Controller
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
            return View(db.GrupeProizvoda.ToList());
        }

        // Index i Details akcije su vidljive korisnicima u ulogama Admin i Korisnik
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupaProizvoda grupaProizvoda = db.GrupeProizvoda.Find(id);
            if (grupaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(grupaProizvoda);
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
        public ActionResult Create([Bind(Include = "GrupaProizvodaID,NazivGrupe")] GrupaProizvoda grupaProizvoda)
        {
            if (ModelState.IsValid)
            {
                db.GrupeProizvoda.Add(grupaProizvoda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupaProizvoda);
        }

        // Samo admin može pristupiti Edit akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupaProizvoda grupaProizvoda = db.GrupeProizvoda.Find(id);
            if (grupaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(grupaProizvoda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "GrupaProizvodaID,NazivGrupe")] GrupaProizvoda grupaProizvoda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupaProizvoda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupaProizvoda);
        }

        // Samo admin može pristupiti Delete akciji
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupaProizvoda grupaProizvoda = db.GrupeProizvoda.Find(id);
            if (grupaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(grupaProizvoda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            GrupaProizvoda grupaProizvoda = db.GrupeProizvoda.Find(id);
            db.GrupeProizvoda.Remove(grupaProizvoda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}