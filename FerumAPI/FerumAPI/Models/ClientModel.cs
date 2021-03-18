using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class ClientModel
    {
        public ClientModel(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            Login = client.Login;
            Password = client.Password;
            //Courier = client.Courier.ToList().ConvertAll(p => new CourierModel(p));
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> Login { get; set; }
        public Nullable<System.Guid> Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //[JsonIgnore]
        //public List<CourierModel> Courier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public List<OrderModel> Order { get; set; }
    }
}