using RVASIspit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RVASIspit.Controllers
{
    public class ProizvodController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        // GET: Proizvodi
        public async Task<ActionResult> Proizvodi()
        {
            var proizvodi = db.Proizvodi.Include(p => p.GrupaProizvoda).Include(p => p.VrstaProizvoda);
            return View(await proizvodi.ToListAsync());
        }

        // GET: Proizvod/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = await db.Proizvodi.FindAsync(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // GET: Proizvod/Create
        public ActionResult Create()
        {
            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe");
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste");
            return View();
        }

        // POST: Proizvod/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProizvodID,Naziv,Cena,GrupaProizvodaID,VrstaProizvodaID")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Proizvodi.Add(proizvod);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe", proizvod.GrupaProizvodaID);
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste", proizvod.VrstaProizvodaID);
            return View(proizvod);
        }

        // GET: Proizvod/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = await db.Proizvodi.FindAsync(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe", proizvod.GrupaProizvodaID);
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste", proizvod.VrstaProizvodaID);
            return View(proizvod);
        }

        // POST: Proizvod/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProizvodID,Naziv,Cena,GrupaProizvodaID,VrstaProizvodaID")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvod).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe", proizvod.GrupaProizvodaID);
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste", proizvod.VrstaProizvodaID);
            return View(proizvod);
        }

        // GET: Proizvod/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = await db.Proizvodi.FindAsync(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // POST: Proizvod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Proizvod proizvod = await db.Proizvodi.FindAsync(id);
            db.Proizvodi.Remove(proizvod);
            await db.SaveChangesAsync();
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