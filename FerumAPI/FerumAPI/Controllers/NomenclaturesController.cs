using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FerumAPI.Entities;
using FerumAPI.Models;

namespace FerumAPI.Controllers
{
    public class NomenclaturesController : ApiController
    {
        private FerumEntities db = new FerumEntities();

        // GET: api/Nomenclatures
        public IHttpActionResult GetNomenclature()
        {
            return Ok(db.Nomenclature.ToList().ConvertAll(p => new NomenclatureModel(p)));
        }

        // GET: api/Nomenclatures/5
        [ResponseType(typeof(Nomenclature))]
        public async Task<IHttpActionResult> GetNomenclature(string id)
        {
            Nomenclature nomenclature = await db.Nomenclature.FindAsync(id);
            if (nomenclature == null)
            {
                return NotFound();
            }

            return Ok(nomenclature);
        }

        // PUT: api/Nomenclatures/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNomenclature(string id, Nomenclature nomenclature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nomenclature.Id)
            {
                return BadRequest();
            }

            db.Entry(nomenclature).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NomenclatureExists(id))
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

        // POST: api/Nomenclatures
        [ResponseType(typeof(Nomenclature))]
        public async Task<IHttpActionResult> PostNomenclature(Nomenclature nomenclature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nomenclature.Add(nomenclature);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NomenclatureExists(nomenclature.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nomenclature.Id }, nomenclature);
        }

        // DELETE: api/Nomenclatures/5
        [ResponseType(typeof(Nomenclature))]
        public async Task<IHttpActionResult> DeleteNomenclature(string id)
        {
            Nomenclature nomenclature = await db.Nomenclature.FindAsync(id);
            if (nomenclature == null)
            {
                return NotFound();
            }

            db.Nomenclature.Remove(nomenclature);
            await db.SaveChangesAsync();

            return Ok(nomenclature);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NomenclatureExists(string id)
        {
            return db.Nomenclature.Count(e => e.Id == id) > 0;
        }
    }
}