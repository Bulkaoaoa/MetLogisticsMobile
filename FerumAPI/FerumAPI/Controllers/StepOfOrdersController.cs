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
    public class StepOfOrdersController : ApiController
    {
        private FerumEntities db = new FerumEntities();

        // GET: api/StepOfOrders
        public IQueryable<StepOfOrder> GetStepOfOrder()
        {
            return db.StepOfOrder;
        }

        [ResponseType(typeof(StepOfOrder))]
        [Route("api/Courier/GetStep")]
        public IHttpActionResult GetStepOfOrder(string orderId)
        {
            List<StepOfOrder> stepOfOrderList = db.StepOfOrder.ToList().Where(p => p.OrderId == orderId).ToList();
            if (stepOfOrderList.Count == 0)
            {
                return NotFound();
            }
            return Ok(new StepOfOrderModel(stepOfOrderList.FirstOrDefault(p => p.isDone == false), true));
        }
        [ResponseType(typeof(Trouble))]
        [Route("api/Courier/PostTrouble")]
        public IHttpActionResult PostTrouble(Trouble trouble)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Trouble.Add(trouble);
            return CreatedAtRoute("DefaultApi", new { id = trouble.Id }, trouble);
        }

        // GET: api/StepOfOrders/5
        [ResponseType(typeof(StepOfOrder))]
        public async Task<IHttpActionResult> GetStepOfOrder(int id)
        {
            StepOfOrder stepOfOrder = await db.StepOfOrder.FindAsync(id);
            if (stepOfOrder == null)
            {
                return NotFound();
            }

            return Ok(stepOfOrder);
        }

        // PUT: api/StepOfOrders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStepOfOrder(int id, StepOfOrder stepOfOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stepOfOrder.Id)
            {
                return BadRequest();
            }

            db.Entry(stepOfOrder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepOfOrderExists(id))
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

        // POST: api/StepOfOrders
        [ResponseType(typeof(StepOfOrder))]
        public  IHttpActionResult PostStepOfOrder(string orderId)
        {
            List<Proccess> proccesses = db.Proccess.ToList();
            List<NomenclatureOfOrder> nomenclatureOfOrders = new List<NomenclatureOfOrder>();
            List<NomenclatureOfOrder> nomenclatureOfOrdersNew = new List<NomenclatureOfOrder>();
            List<NomenclatureOfWarehouse> nomenclatureOfWarehouses = new List<NomenclatureOfWarehouse>();
            List<StepOfOrder> stepOfOrders = new List<StepOfOrder>();
            List<Trouble> troubles = new List<Trouble>();
            List<TypeOfTrouble> typeOfTroubles = new List<TypeOfTrouble>();
            nomenclatureOfOrders = db.Order.ToList().FirstOrDefault(p => p.Id == orderId).NomenclatureOfOrder.ToList();
            nomenclatureOfWarehouses = db.NomenclatureOfWarehouse.ToList();
            nomenclatureOfOrdersNew.AddRange(nomenclatureOfOrders);
            typeOfTroubles = db.TypeOfTrouble.ToList();
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                OrderId = orderId,
                ProcessId = 1,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 1),
                DateOfStart = DateTime.UtcNow.AddHours(3)
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                OrderId = orderId,
                ProcessId = 2,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 2),
            });
            while (nomenclatureOfOrders.Count > 0)
            {
                foreach (var item in nomenclatureOfOrders)
                {
                    var list = nomenclatureOfWarehouses.Where(p => p.Nomenclature == item.Nomenclature && p.Count > 0).ToList().OrderBy(p => p.Cell.WarehouseId).ToList();
                    if (list.Count == 0)
                    {
                        troubles.Add(new Trouble
                        {
                            Description = $"{item.Nomenclature.Name} отсутствует на складе полностью или частично\nНе нашлось {item.Count} тонн",
                            StepOfOrder = stepOfOrders[0],
                            TypeOfTrouble = typeOfTroubles[0],
                            TypeOfTroubleId = 1,
                        });
                        nomenclatureOfOrdersNew.Remove(item);
                        continue;
                    }
                    var nomenclatureOfWarehouse = list.FirstOrDefault(p => p.Count >= item.Count);
                    if (nomenclatureOfWarehouse != null)
                    {
                        stepOfOrders.Add(CreateShipment(orderId, item, nomenclatureOfWarehouse, item.Count, proccesses));
                        list.FirstOrDefault(p => p == nomenclatureOfWarehouse).Count -= item.Count;
                        nomenclatureOfOrdersNew.Remove(item);
                    }
                    else
                    {
                        nomenclatureOfWarehouse = list.FirstOrDefault();
                        stepOfOrders.Add(CreateShipment(orderId, item, nomenclatureOfWarehouse, nomenclatureOfWarehouse.Count, proccesses));
                        list.FirstOrDefault(p => p == nomenclatureOfWarehouse).Count = 0;
                    }
                }
                nomenclatureOfOrders.Clear();
                nomenclatureOfOrders.AddRange(nomenclatureOfOrdersNew);
            }
            var warehouse = new Warehouse();
            foreach (var item in stepOfOrders.Skip(2).OrderBy(p => p.Shipment.NomenclatureOfWarehouse.Cell.WarehouseId).ToList())
            {
                var currentWarehouse = item.Shipment.NomenclatureOfWarehouse.Cell.Warehouse;
                if (currentWarehouse != warehouse)
                {
                    var move = new Movement
                    {
                        Warehouse = currentWarehouse,
                        WarehouseId = currentWarehouse.Id
                    };
                    stepOfOrders.Insert(stepOfOrders.IndexOf(item), new StepOfOrder
                    {
                        isDone = false,
                        Movement = move,
                        OrderId = orderId,
                        ProcessId = 3,
                        Proccess = proccesses.FirstOrDefault(p => p.Id == 3),
                    });
                    warehouse = currentWarehouse;
                }
            }
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                OrderId = orderId,
                ProcessId = 9,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 9),
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                OrderId = orderId,
                ProcessId = 7,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 7),
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                OrderId = orderId,
                ProcessId = 8,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 8),
            });
            db.StepOfOrder.AddRange(stepOfOrders);
            db.Trouble.AddRange(troubles);
            db.SaveChanges();
            return Ok(stepOfOrders.ToList().ConvertAll(p=> new StepOfOrderModel(p, true)));
        }

        private static StepOfOrder CreateShipment(string orderId, NomenclatureOfOrder item, NomenclatureOfWarehouse nomenclatureOfWarehouse, decimal count, List<Proccess> proccesses)
        {
            var shipment = new Shipment
            {
                NomenclatureOfWarehouse = nomenclatureOfWarehouse,
                Count = count,
                NomenclatureOfWarehouseId = nomenclatureOfWarehouse.Id
            };
            var step = new StepOfOrder
            {
                isDone = false,
                OrderId = orderId,
                ProcessId = 4,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 4),
                Shipment = shipment,
            };
            return step;
        }

        // DELETE: api/StepOfOrders/5
        [ResponseType(typeof(StepOfOrder))]
        public async Task<IHttpActionResult> DeleteStepOfOrder(int id)
        {
            StepOfOrder stepOfOrder = await db.StepOfOrder.FindAsync(id);
            if (stepOfOrder == null)
            {
                return NotFound();
            }

            db.StepOfOrder.Remove(stepOfOrder);
            await db.SaveChangesAsync();

            return Ok(stepOfOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StepOfOrderExists(int id)
        {
            return db.StepOfOrder.Count(e => e.Id == id) > 0;
        }
    }
}