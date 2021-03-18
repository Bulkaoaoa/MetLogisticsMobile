using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FerumAPI.Entities;
using FerumAPI.Models;

namespace FerumAPI.Controllers
{
    public class TroublesController : ApiController
    {
        private FerumEntities db = new FerumEntities();

        // GET: api/Troubles
        public IHttpActionResult GetTrouble()
        {
            return Ok(db.Trouble.ToList().ConvertAll(p=> new TroubleModel(p)));
        }

        // GET: api/Troubles/5
        [ResponseType(typeof(Trouble))]
        public IHttpActionResult GetTrouble(int id)
        {
            Trouble trouble = db.Trouble.Find(id);
            if (trouble == null)
            {
                return NotFound();
            }

            return Ok(trouble);
        }

        // PUT: api/Troubles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrouble(int id, Trouble trouble)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trouble.Id)
            {
                return BadRequest();
            }

            db.Entry(trouble).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TroubleExists(id))
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

        // POST: api/Troubles
        [ResponseType(typeof(Trouble))]
        public IHttpActionResult PostTrouble(Trouble trouble)
        {
            db.Trouble.Add(trouble);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trouble.Id }, trouble);
        }

        // DELETE: api/Troubles/5
        [ResponseType(typeof(Trouble))]
        public IHttpActionResult DeleteTrouble(int id)
        {
            Trouble trouble = db.Trouble.Find(id);
            if (trouble == null)
            {
                return NotFound();
            }

            db.Trouble.Remove(trouble);
            db.SaveChanges();

            return Ok(trouble);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TroubleExists(int id)
        {
            return db.Trouble.Count(e => e.Id == id) > 0;
        }
    }
}