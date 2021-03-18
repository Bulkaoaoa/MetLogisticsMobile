using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class WarehouseModel
    {
        public WarehouseModel(Warehouse warehouse)
        {
            Id = warehouse.Id;
            Name = warehouse.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Nullable<int> StorekeeperId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<CellModel> Cell { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<MovementStorekeeper> MovementStorekeeper { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<MovementStorekeeper> MovementStorekeeper1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual List<Queue> Queue { get; set; }
        public virtual Storekeeper Storekeeper { get; set; }
    }
}