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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Zaposleni
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Korisnik"))
            {
                return View(db.Zaposleni.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
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
            if (User.IsInRole("Admin") || User.IsInRole("Korisnik"))
            {
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Return 403 Forbidden for unauthorized users
            }
        }

        // POST: Zaposleni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZaposleniID,Ime,Prezime")] Zaposleni zaposleni)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Korisnik"))
            {
                if (ModelState.IsValid)
                {
                    db.Zaposleni.Add(zaposleni);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(zaposleni);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Return 403 Forbidden for unauthorized users
            }
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
            if (User.IsInRole("Admin") || User.IsInRole("Korisnik"))
            {
                return View(zaposleni);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Return 403 Forbidden for unauthorized users
            }
        }

        // POST: Zaposleni/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZaposleniID,Ime,Prezime")] Zaposleni zaposleni)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Korisnik"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(zaposleni).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(zaposleni);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Return 403 Forbidden for unauthorized users
            }
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
            if (User.IsInRole("Admin") || User.IsInRole("Korisnik"))
            {
                return View(zaposleni);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Return 403 Forbidden for unauthorized users
            }
        }

        // POST: Zaposleni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Korisnik"))
            {
                Zaposleni zaposleni = db.Zaposleni.Find(id);
                db.Zaposleni.Remove(zaposleni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Return 403 Forbidden for unauthorized users
            }
        }
    }
}