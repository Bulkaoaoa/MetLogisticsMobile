using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class StepOfOrderModel
    {
        public StepOfOrderModel(StepOfOrder stepOfOrder)
        {
            Id = stepOfOrder.Id;
            Proccess = new ProccessModel(stepOfOrder.Proccess);
            isDone = stepOfOrder.isDone;
            DateOfStart = stepOfOrder.DateOfStart;
            DateOfEnd = stepOfOrder.DateOfEnd;
            if (stepOfOrder.Movement != null)
                Movement = new MovementModel(stepOfOrder.Movement);
            if (stepOfOrder.Shipment != null)
                Shipment = new ShipmentModel(stepOfOrder.Shipment);
            Trouble = stepOfOrder.Trouble.ToList().ConvertAll(p => new TroubleModel(p, true));
            TimeSpent = stepOfOrder.TimeSpent;
        }
        [JsonIgnore]
        public int ProcessId { get; set; }
        [JsonIgnore]
        public string OrderId { get; set; }
        [JsonIgnore]
        public Nullable<int> MovementId { get; set; }
        [JsonIgnore]
        public Nullable<int> ShipmentId { get; set; }
        public int Id { get; set; }
        public bool isDone { get; set; }
        public Nullable<int> TimeSpent { get; set; }
        public Nullable<System.DateTime> DateOfStart { get; set; }
        public Nullable<System.DateTime> DateOfEnd { get; set; }
        public MovementModel Movement { get; set; }
        [JsonIgnore]
        public OrderModel Order { get; set; }
        public ProccessModel Proccess { get; set; }
        public ShipmentModel Shipment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<TroubleModel> Trouble { get; set; }
    }
}