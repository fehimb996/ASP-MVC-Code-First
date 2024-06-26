using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using RVASIspit.Models;

namespace RVASIspit.Controllers
{
    public class ZaposleniApiController : ApiController
    {
        private CodeFirstBaza db = new CodeFirstBaza();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZaposleniExists(int id)
        {
            return db.Zaposleni.Count(e => e.ZaposleniID == id) > 0;
        }

        [Route("api/ZaposleniApi")]
        public IQueryable<Zaposleni> GetZaposleni()
        {
            return db.Zaposleni;
        }

        [Route("api/ZaposleniApi/{id:int}")]
        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult GetZaposleni(int id)
        {
            Zaposleni zaposleni = db.Zaposleni.Find(id);
            if (zaposleni == null)
            {
                return NotFound();
            }

            return Ok(zaposleni);
        }

        [Route("api/ZaposleniApi/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutZaposleni(int id, Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zaposleni.ZaposleniID)
            {
                return BadRequest();
            }

            db.Entry(zaposleni).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZaposleniExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/ZaposleniApi")]
        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult PostZaposleni(Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Zaposleni.Add(zaposleni);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + zaposleni.ZaposleniID), zaposleni);
        }

        [Route("api/ZaposleniApi/{id:int}")]
        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult DeleteZaposleni(int id)
        {
            Zaposleni zaposleni = db.Zaposleni.Find(id);
            if (zaposleni == null)
            {
                return NotFound();
            }

            db.Zaposleni.Remove(zaposleni);
            db.SaveChanges();

            return Ok(zaposleni);
        }
    }
}