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

        public ActionResult Index()
        {
            return View(db.GrupeProizvoda.ToList());
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult DeleteConfirmed(int id)
        {
            GrupaProizvoda grupaProizvoda = db.GrupeProizvoda.Find(id);
            db.GrupeProizvoda.Remove(grupaProizvoda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}