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

        public ActionResult Index()
        {
            return View(db.Sastojci.ToList());
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult DeleteConfirmed(int id)
        {
            Sastojak sastojak = db.Sastojci.Find(id);
            db.Sastojci.Remove(sastojak);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}