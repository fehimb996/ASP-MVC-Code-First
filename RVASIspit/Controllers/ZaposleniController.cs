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
    public class ZaposleniController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        // GET: Zaposleni
        public ActionResult Zaposleni()
        {
            return View(db.Zaposleni.ToList());
        }

        // GET: Zaposleni/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = db.Zaposleni.Find(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(zaposleni);
        }

        // GET: Zaposleni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zaposleni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZaposleniID,Ime,Prezime")] Zaposleni zaposleni)
        {
            if (ModelState.IsValid)
            {
                db.Zaposleni.Add(zaposleni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zaposleni);
        }

        // GET: Zaposleni/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = db.Zaposleni.Find(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(zaposleni);
        }

        // POST: Zaposleni/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZaposleniID,Ime,Prezime")] Zaposleni zaposleni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zaposleni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zaposleni);
        }

        // GET: Zaposleni/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = db.Zaposleni.Find(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(zaposleni);
        }

        // POST: Zaposleni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zaposleni zaposleni = db.Zaposleni.Find(id);
            db.Zaposleni.Remove(zaposleni);
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