using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class CourierModel
    {
        public CourierModel(Courier courier)
        {
            if (courier != null)
            {
                Id = courier.Id;
                TelephoneNumber = courier.TelephoneNumber;
                FIO = courier.FIO;
                CarNumber = courier.CarNumber;
                Password = courier.Password;
                Client = new ClientModel(courier.Client);
                Order = courier.Order.ToList().ConvertAll(p => new OrderModel(p)).ToList();
            }
        }

        public int Id { get; set; }
        public string TelephoneNumber { get; set; }
        public string FIO { get; set; }
        public string CarNumber { get; set; }
        public decimal CarCarrying { get; set; }
        public System.Guid Password { get; set; }
        [JsonIgnore]
        public string ClientId { get; set; }

        public ClientModel Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  List<OrderModel> Order { get; set; }
    }
}