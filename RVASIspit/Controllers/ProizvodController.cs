using PagedList;
using RVASIspit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList.Mvc;

namespace RVASIspit.Controllers
{
    public class ProizvodController : Controller
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? grupaProizvodaId, int? vrstaProizvodaId, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.CurrentFilter = searchString;

            var proizvodi = from p in db.Proizvodi.Include(p => p.GrupaProizvoda).Include(p => p.VrstaProizvoda)
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                proizvodi = proizvodi.Where(p => p.Naziv.Contains(searchString));
            }

            if (grupaProizvodaId.HasValue)
            {
                proizvodi = proizvodi.Where(p => p.GrupaProizvodaID == grupaProizvodaId.Value);
            }

            if (vrstaProizvodaId.HasValue)
            {
                proizvodi = proizvodi.Where(p => p.VrstaProizvodaID == vrstaProizvodaId.Value);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    proizvodi = proizvodi.OrderByDescending(p => p.Naziv);
                    break;
                case "Price":
                    proizvodi = proizvodi.OrderBy(p => p.Cena);
                    break;
                case "price_desc":
                    proizvodi = proizvodi.OrderByDescending(p => p.Cena);
                    break;
                default:
                    proizvodi = proizvodi.OrderBy(p => p.Naziv);
                    break;
            }

            int pageSize = 12;
            int pageNumber = (page ?? 1);

            var grupeProizvoda = db.GrupeProizvoda.ToList();
            var vrsteProizvoda = db.VrsteProizvoda.ToList();

            ViewBag.GrupaProizvodaID = new SelectList(grupeProizvoda, "GrupaProizvodaID", "NazivGrupe");
            ViewBag.VrstaProizvodaID = new SelectList(vrsteProizvoda, "VrstaProizvodaID", "NazivVrste");

            return View(proizvodi.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Include(p => p.GrupaProizvoda).Include(p => p.VrstaProizvoda).FirstOrDefault(p => p.ProizvodID == id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe");
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProizvodID,Naziv,Cena,GrupaProizvodaID,VrstaProizvodaID")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Proizvodi.Add(proizvod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe", proizvod.GrupaProizvodaID);
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste", proizvod.VrstaProizvodaID);
            return View(proizvod);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe", proizvod.GrupaProizvodaID);
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste", proizvod.VrstaProizvodaID);
            return View(proizvod);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProizvodID,Naziv,Cena,GrupaProizvodaID,VrstaProizvodaID")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupaProizvodaID = new SelectList(db.GrupeProizvoda, "GrupaProizvodaID", "NazivGrupe", proizvod.GrupaProizvodaID);
            ViewBag.VrstaProizvodaID = new SelectList(db.VrsteProizvoda, "VrstaProizvodaID", "NazivVrste", proizvod.VrstaProizvodaID);
            return View(proizvod);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Proizvod proizvod = db.Proizvodi
                                  .Include(p => p.GrupaProizvoda)
                                  .Include(p => p.VrstaProizvoda)
                                  .FirstOrDefault(p => p.ProizvodID == id);

            if (proizvod == null)
            {
                return HttpNotFound();
            }

            return View(proizvod);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvod proizvod = db.Proizvodi.Find(id);
            db.Proizvodi.Remove(proizvod);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}