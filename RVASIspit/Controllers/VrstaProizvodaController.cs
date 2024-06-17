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

        // GET: VrsteProizvoda
        public ActionResult Index()
        {
            return View(db.VrsteProizvoda.ToList());
        }

        // GET: VrstaProizvoda/Details/5
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

        // GET: VrstaProizvoda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VrstaProizvoda/Create
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

        // GET: VrstaProizvoda/Edit/5
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

        // POST: VrstaProizvoda/Edit/5
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

        // GET: VrstaProizvoda/Delete/5
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

        // POST: VrstaProizvoda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VrstaProizvoda vrstaProizvoda = db.VrsteProizvoda.Find(id);
            db.VrsteProizvoda.Remove(vrstaProizvoda);
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