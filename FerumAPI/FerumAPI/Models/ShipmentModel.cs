using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class ShipmentModel
    {
        public ShipmentModel(Shipment shipment)
        {
            Id = shipment.Id;
            Count = shipment.Count;
            NomenclatureOfWarehouse = new NomenclatureOfWarehouseModel(shipment.NomenclatureOfWarehouse);
        }
        public int Id { get; set; }
        public decimal Count { get; set; }

        public NomenclatureOfWarehouseModel NomenclatureOfWarehouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public List<StepOfOrder> StepOfOrder { get; set; }
    }
}