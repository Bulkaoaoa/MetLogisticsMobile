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
using FerumAPI.Classes;
using FerumAPI.Entities;
using FerumAPI.Models;

namespace FerumAPI.Controllers
{
    public class ClientsController : ApiController
    {
        private FerumEntities db = new FerumEntities();

        // GET: api/Clients
        public IHttpActionResult GetClient()
        {
            return Ok(db.Client.ToList().ConvertAll(p => new ClientModel(p)));
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(string id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [ResponseType(typeof(Client))]
        [Route("api/Client/OrdersToDay")]
        public IHttpActionResult GetOrders(string clietnId)
        {
            Client client = db.Client.Find(clietnId);
            if (client == null)
            {
                return NotFound();
            }
            List<Order> orderList = new List<Order>();
            orderList.AddRange(client.Order.ToList());
            foreach (var item in client.Order)
            {
                if (item.DateTimeOfArrivle == null)
                    orderList.Remove(item);
            }
            if (orderList.Count > 0)
            return Ok(orderList.Where(p => p.DateTimeOfArrivle.Value.Date == DateTime.UtcNow.AddHours(3).Date)
                .ToList().ConvertAll(p => new OrderModel(p)));
            return NotFound();
        }

        [Route("api/ClientOrCourier/Authorization")]
        public IHttpActionResult GetAuthorization(string login, string password, bool isDriver)
        {
            Client client = db.Client.ToList().FirstOrDefault(p => p.Login == new GetHashString().GetGuid(login)
            && p.Password == new GetHashString().GetGuid(password));
            if (isDriver)
            {
                Courier courier = db.Courier.ToList().FirstOrDefault(p => p.TelephoneNumber == login
                && p.Password == new GetHashString().GetGuid(password));
                if (courier == null)
                {
                    return NotFound();
                }
                return Ok(new CourierModel(courier));
            }
            else
            {
                if (client == null)
                {
                    return NotFound();
                }
                return Ok(new ClientModel(client));
            }
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(string id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.Id)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(string login, string password, string companyName)
        {
            bool isCorrect = false;
            var random = new Random();
            string id = "";
            List<Client> clients = db.Client.ToList();
            while (!isCorrect)
            {
                int countNumbers = random.Next(3, 9);
                string randomNumber = "";
                string letter = "АБВГДЖЗИКЛМНО-ПРСТУФХЦЧШ";
                for (int i = 0; i <= countNumbers; i++)
                {
                    randomNumber += random.Next(0, 10).ToString();
                }
                id = $"{letter[random.Next(0, letter.Length)]}" +
                    $"{letter[random.Next(0, letter.Length)]}{randomNumber}";
                if (clients.FirstOrDefault(p => p.Id == id) == null)
                    isCorrect = true;
            }
            Client client = new Client
            {
                Login = new GetHashString().GetGuid(login),
                Password = new GetHashString().GetGuid(password),
                Name = companyName,
                Id = id
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Client.Add(client);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(string id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(string id)
        {
            return db.Client.Count(e => e.Id == id) > 0;
        }
    }
}