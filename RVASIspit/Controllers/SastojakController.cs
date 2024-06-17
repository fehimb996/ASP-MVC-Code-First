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

        // GET: Sastojci
        public ActionResult Index()
        {
            return View(db.Sastojci.ToList());
        }

        // GET: Sastojak/Details/5
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

        // GET: Sastojak/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sastojak/Create
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

        // GET: Sastojak/Edit/5
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

        // POST: Sastojak/Edit/5
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

        // GET: Sastojak/Delete/5
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

        // POST: Sastojak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sastojak sastojak = db.Sastojci.Find(id);
            db.Sastojci.Remove(sastojak);
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