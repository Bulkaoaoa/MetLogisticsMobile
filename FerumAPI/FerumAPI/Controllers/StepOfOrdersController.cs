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
            List<StepOfOrder> resultStepOfOrdersList = new List<StepOfOrder>();
            if (stepOfOrderList.FirstOrDefault(p => p.isDone == false).Shipment != null)
            {
                foreach (var item in stepOfOrderList.Where(p => p.isDone == false).ToList())
                {
                    if (item.Shipment == null)
                    {
                        break;
                    }
                    resultStepOfOrdersList.Add(item);
                }
            }
            else
            {
                resultStepOfOrdersList.Add(stepOfOrderList.FirstOrDefault(p => p.isDone == false));
            }
            return Ok(resultStepOfOrdersList.ConvertAll(p => new StepOfOrderModel(p)));
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
        public IHttpActionResult PutStepOfOrder(List<StepOfOrder> stepOfOrders)
        {
            List<StepOfOrder> bufferStepOfOrder = new List<StepOfOrder>();
            foreach (var item in stepOfOrders)
            {
                bufferStepOfOrder.Add(db.StepOfOrder.ToList().FirstOrDefault(p => p.Id == item.Id));
            }
            foreach (var item in bufferStepOfOrder)
            {
                item.DateOfEnd = DateTime.UtcNow.AddHours(3);
                item.isDone = true;
                item.TimeSpent = Convert.ToInt32((item.DateOfEnd.Value - item.DateOfStart.Value).TotalMinutes);
            }
            db.SaveChanges();
            List<StepOfOrder> list = db.StepOfOrder.ToList().Where(p => p.Order.Id == stepOfOrders.FirstOrDefault().Order.Id).ToList();
            if (list.Where(p => p.isDone == false).ToList().Count == 0)
            {
                return NotFound();
            }
            if (list.FirstOrDefault(p => p.isDone == false).Shipment != null)
            {
                foreach (var item in list.Where(p => p.isDone == false).ToList())
                {
                    if (item.Shipment == null)
                    {
                        break;
                    }
                    item.DateOfStart = DateTime.UtcNow.AddHours(3);
                }
            }
            else
            {
                list.FirstOrDefault(p => p.isDone == false).DateOfStart = DateTime.UtcNow.AddHours(3);
            }
            db.SaveChanges();
            return GetStepOfOrder(list[0].Order.Id);
        }

        // POST: api/StepOfOrders
        [ResponseType(typeof(StepOfOrder))]
        public IHttpActionResult PostStepOfOrder(string orderId)
        {
            List<Proccess> proccesses = db.Proccess.ToList();
            List<NomenclatureOfOrder> nomenclatureOfOrders = new List<NomenclatureOfOrder>();
            List<NomenclatureOfOrder> nomenclatureOfOrdersNew = new List<NomenclatureOfOrder>();
            List<NomenclatureOfWarehouse> nomenclatureOfWarehouses = new List<NomenclatureOfWarehouse>();
            List<StepOfOrder> stepOfOrders = new List<StepOfOrder>();
            List<Trouble> troubles = new List<Trouble>();
            List<TypeOfTrouble> typeOfTroubles = new List<TypeOfTrouble>();
            Order order = db.Order.ToList().FirstOrDefault(p => p.Id == orderId);
            nomenclatureOfOrders = order.NomenclatureOfOrder.ToList();
            nomenclatureOfWarehouses = db.NomenclatureOfWarehouse.ToList();
            nomenclatureOfOrdersNew.AddRange(nomenclatureOfOrders);
            typeOfTroubles = db.TypeOfTrouble.ToList();
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 1,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 1),
                DateOfStart = DateTime.UtcNow.AddHours(3)
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 2,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 2),
            });
            while (nomenclatureOfOrders.Count > 0)
            {
                foreach (var item in nomenclatureOfOrders)
                {
                    var list = nomenclatureOfWarehouses.Where(p => p.Nomenclature == item.Nomenclature && p.Count - item.Count >= 0).ToList().OrderBy(p => p.Cell.WarehouseId).ToList();
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
                        stepOfOrders.Add(CreateShipment(order, item, nomenclatureOfWarehouse, item.Count, proccesses));
                        list.FirstOrDefault(p => p == nomenclatureOfWarehouse).Count -= item.Count;
                        nomenclatureOfOrdersNew.Remove(item);
                    }
                    else
                    {
                        nomenclatureOfWarehouse = list.FirstOrDefault();
                        stepOfOrders.Add(CreateShipment(order, item, nomenclatureOfWarehouse, nomenclatureOfWarehouse.Count, proccesses));
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
                        Order = order,
                        ProcessId = 3,
                        Proccess = proccesses.FirstOrDefault(p => p.Id == 3),
                    });
                    stepOfOrders.Insert(stepOfOrders.IndexOf(item), new StepOfOrder
                    {
                        isDone = false,
                        Order = order,
                        ProcessId = 6,
                        Proccess = proccesses.FirstOrDefault(p => p.Id == 6),
                    });
                    warehouse = currentWarehouse;
                }
            }
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 9,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 9),
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 7,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 7),
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 8,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 8),
            });
            db.StepOfOrder.AddRange(stepOfOrders);
            db.Trouble.AddRange(troubles);
            db.SaveChanges();
            return Ok(stepOfOrders.ToList().ConvertAll(p => new StepOfOrderModel(p)));
        }
        [Route ("api/Courier/GetNewStep")]
        public IHttpActionResult GetStepOfOrderNew(string orderId)
        {
            List<Proccess> proccesses = db.Proccess.ToList();
            List<NomenclatureOfOrder> nomenclatureOfOrders = new List<NomenclatureOfOrder>();
            List<NomenclatureOfOrder> nomenclatureOfOrdersNew = new List<NomenclatureOfOrder>();
            List<NomenclatureOfWarehouse> nomenclatureOfWarehouses = new List<NomenclatureOfWarehouse>();
            List<StepOfOrder> stepOfOrders = new List<StepOfOrder>();
            List<Trouble> troubles = new List<Trouble>();
            List<TypeOfTrouble> typeOfTroubles = new List<TypeOfTrouble>();
            Order order = db.Order.ToList().FirstOrDefault(p => p.Id == orderId);
            nomenclatureOfOrders = order.NomenclatureOfOrder.ToList();
            nomenclatureOfWarehouses = db.NomenclatureOfWarehouse.ToList();
            nomenclatureOfOrdersNew.AddRange(nomenclatureOfOrders);
            typeOfTroubles = db.TypeOfTrouble.ToList();
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 1,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 1),
                DateOfStart = DateTime.UtcNow.AddHours(3)
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 2,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 2),
            });
            while (nomenclatureOfOrders.Count > 0)
            {
                foreach (var item in nomenclatureOfOrders)
                {
                    var list = nomenclatureOfWarehouses.Where(p => p.Nomenclature == item.Nomenclature && p.Count - item.Count >= 0).ToList().OrderBy(p => p.Cell.WarehouseId).ToList();
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
                        stepOfOrders.Add(CreateShipment(order, item, nomenclatureOfWarehouse, item.Count, proccesses));
                        list.FirstOrDefault(p => p == nomenclatureOfWarehouse).Count -= item.Count;
                        nomenclatureOfOrdersNew.Remove(item);
                    }
                    else
                    {
                        nomenclatureOfWarehouse = list.FirstOrDefault();
                        stepOfOrders.Add(CreateShipment(order, item, nomenclatureOfWarehouse, nomenclatureOfWarehouse.Count, proccesses));
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
                        Order = order,
                        ProcessId = 3,
                        Proccess = proccesses.FirstOrDefault(p => p.Id == 3),
                    });
                    stepOfOrders.Insert(stepOfOrders.IndexOf(item), new StepOfOrder
                    {
                        isDone = false,
                        Order = order,
                        ProcessId = 6,
                        Proccess = proccesses.FirstOrDefault(p => p.Id == 6),
                    });
                    warehouse = currentWarehouse;
                }
            }
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 9,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 9),
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 7,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 7),
            });
            //Хорошо бы это в метод, но мне лень думать дальше
            stepOfOrders.Add(new StepOfOrder
            {
                isDone = false,
                Order = order,
                ProcessId = 8,
                Proccess = proccesses.FirstOrDefault(p => p.Id == 8),
            });
            db.StepOfOrder.AddRange(stepOfOrders);
            db.Trouble.AddRange(troubles);
            db.SaveChanges();
            return Ok(stepOfOrders.ToList().ConvertAll(p => new StepOfOrderModel(p)));
        }
        private static StepOfOrder CreateShipment(Order order, NomenclatureOfOrder item, NomenclatureOfWarehouse nomenclatureOfWarehouse, decimal count, List<Proccess> proccesses)
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
                Order = order,
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