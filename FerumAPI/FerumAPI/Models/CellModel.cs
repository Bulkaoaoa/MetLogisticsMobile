using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class CellModel
    {
        public CellModel(Cell cell, bool viewNomenclatureWarehouse, bool viewWarehouse)
        {
            Id = cell.Id;
            Address = cell.Address;
            if (viewWarehouse)
                Warehouse = new WarehouseModel(cell.Warehouse, false);
            if (viewNomenclatureWarehouse)
                NomenclatureOfWarehouse = cell.NomenclatureOfWarehouse.ToList().ConvertAll(p => new NomenclatureOfWarehouseModel(p, false));
        }

        public int Id { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public int WarehouseId { get; set; }
        public  WarehouseModel Warehouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NomenclatureOfWarehouseModel> NomenclatureOfWarehouse { get; set; }
    }
}