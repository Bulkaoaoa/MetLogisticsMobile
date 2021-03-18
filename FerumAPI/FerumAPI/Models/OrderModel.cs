using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class OrderModel
    {
        public OrderModel(Order order)
        {
            Id = order.Id;
            DateOfCreate = order.DateOfCreate;
            Client = new ClientModel(order.Client);
            Effectiveness = order.Effectiveness;
            DateTimeOfArrivle = order.DateTimeOfArrivle;
            NomenclatureOfOrder = order.NomenclatureOfOrder.ToList().ConvertAll(p => new NomenclatureOfOrderModel(p));
            //QueueOfWarehouse = order.QueueOfWarehouse.ToList();
            //StepOfOrder = order.StepOfOrder.ToList();


        }
        public string Id { get; set; }
        public ClientModel Client { get; set; }
        public System.DateTime DateOfCreate { get; set; }
        [JsonIgnore]
        public Nullable<int> CourierId { get; set; }
        public Nullable<decimal> Effectiveness { get; set; }
        public Nullable<System.DateTime> DateTimeOfArrivle { get; set; }

        [JsonIgnore]
        public string ClientId { get; set; }
        public CourierModel Courier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NomenclatureOfOrderModel> NomenclatureOfOrder { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public List<QueueOfWarehouse> QueueOfWarehouse { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public List<StepOfOrder> StepOfOrder { get; set; }
    }
}