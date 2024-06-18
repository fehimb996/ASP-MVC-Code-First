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

        public ActionResult Index()
        {
            return View(db.VrsteProizvoda.ToList());
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult DeleteConfirmed(int id)
        {
            VrstaProizvoda vrstaProizvoda = db.VrsteProizvoda.Find(id);
            db.VrsteProizvoda.Remove(vrstaProizvoda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}