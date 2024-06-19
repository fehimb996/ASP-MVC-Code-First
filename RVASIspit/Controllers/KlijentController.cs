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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "KlijentID, Ime, Prezime, Telefon")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                db.Klijenti.Add(klijent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klijent);
        }

        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Klijent klijent = db.Klijenti.Find(id);
            db.Klijenti.Remove(klijent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Korisnik")]
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

            // Provera da li je korisnik u ulozi "Admin" ili "Korisnik"
            if (!User.IsInRole("Admin") && !User.IsInRole("Korisnik"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Ovde može biti i HttpNotFound()
            }

            return View(klijent);
        }

        [Authorize(Roles = "Admin, Korisnik")]

        // Index akcija vidljiva korisnicima sa ulogom Admin ili Korisnik
        public ActionResult Index()
        {
            var klijenti = db.Klijenti.ToList();
            return View(klijenti);
        }
    }
}