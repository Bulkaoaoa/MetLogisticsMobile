using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class MovementModel
    {
        public MovementModel(Movement movement)
        {
            Id = movement.Id;
            Warehouse = new WarehouseModel(movement.Warehouse, false);
        }

        public int Id { get; set; }
        [JsonIgnore]
        public int WarehouseId { get; set; }
        public WarehouseModel Warehouse { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StepOfOrder> StepOfOrder { get; set; }
    }
}