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
    public class OrdersController : ApiController
    {
        private FerumEntities db = new FerumEntities();

        // GET: api/Orders
        [ResponseType(typeof(List<OrderModel>))]
        public IHttpActionResult GetOrder()
        {
            return Ok(db.Order.ToList().ConvertAll(p => new OrderModel(p)));
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(string id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(string id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(DateTime dateOfCreate, string clientId, int courierId, DateTime dateTimeOfArrivle, List<NomenclatureOfOrder> nomenclatureOfOrders)
        {
            Order order;
            Order lastOrder = db.Order.ToList().OrderBy(p => p.Id).LastOrDefault();
            string orderId = $"{long.Parse(lastOrder.Id) + 1}";
            int diff = lastOrder.Id.Length - orderId.Length;
            for (int i = 0; i < diff; i++)
            {
                orderId = $"0{orderId}";
            }
            order = new Order
            {
                Id = orderId,
                ClientId = clientId,
                CourierId = courierId,
                DateOfCreate = dateOfCreate,
                DateTimeOfArrivle = dateTimeOfArrivle
            };
            db.Order.Add(order);
            foreach (var nomenclatureOfOrder in nomenclatureOfOrders)
            {
                db.NomenclatureOfOrder.Add(new NomenclatureOfOrder
                {
                    NumenclatureId = nomenclatureOfOrder.NumenclatureId,
                    OrderId = nomenclatureOfOrder.OrderId,
                    Count = nomenclatureOfOrder.Count
                });
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(string id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(string id)
        {
            return db.Order.Count(e => e.Id == id) > 0;
        }
    }
}