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
    public class KlijentController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        // GET: Klijenti
        public ActionResult Index()
        {
            return View(db.Klijenti.ToList());
        }

        // GET: Klijent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // GET: Klijent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klijent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KlijentID,Ime,Prezime,Telefon")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                db.Klijenti.Add(klijent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klijent);
        }

        // GET: Klijent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // POST: Klijent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KlijentID,Ime,Prezime,Telefon")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klijent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klijent);
        }

        // GET: Klijent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // POST: Klijent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klijent klijent = db.Klijenti.Find(id);
            db.Klijenti.Remove(klijent);
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