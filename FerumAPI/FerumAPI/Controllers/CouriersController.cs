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
    public class CouriersController : ApiController
    {
        private FerumEntities db = new FerumEntities();

        // GET: api/Couriers
        public IQueryable<Courier> GetCourier()
        {
            return db.Courier;
        }

        // GET: api/Couriers/5
        [ResponseType(typeof(Courier))]
        public IHttpActionResult GetCourier(int id)
        {
            Courier courier = db.Courier.Find(id);
            if (courier == null)
            {
                return NotFound();
            }

            return Ok(courier);
        }
        [Route("api/Courier/GetOrder")]
        public IHttpActionResult GetCourierOrder(int courierId)
        {
            List<Order> orders = new List<Order>();
            Courier courier = db.Courier.Find(courierId);
            if (courier == null)
            {
                return NotFound();
            }
            orders = db.Order.ToList().Where(p => p.CourierId == courierId).ToList();
            return Ok(orders.ConvertAll(p => new OrderModel(p)));
        }

        // PUT: api/Couriers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourier(int id, Courier courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courier.Id)
            {
                return BadRequest();
            }

            db.Entry(courier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourierExists(id))
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

        // POST: api/Couriers
        [ResponseType(typeof(Courier))]
        public IHttpActionResult PostCourier(Courier courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courier.Add(courier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = courier.Id }, courier);
        }

        // DELETE: api/Couriers/5
        [ResponseType(typeof(Courier))]
        public IHttpActionResult DeleteCourier(int id)
        {
            Courier courier = db.Courier.Find(id);
            if (courier == null)
            {
                return NotFound();
            }

            db.Courier.Remove(courier);
            db.SaveChanges();

            return Ok(courier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourierExists(int id)
        {
            return db.Courier.Count(e => e.Id == id) > 0;
        }
    }
}