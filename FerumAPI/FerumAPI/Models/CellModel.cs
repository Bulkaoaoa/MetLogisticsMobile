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
        public CellModel(Cell cell)
        {
            Id = cell.Id;
            Address = cell.Address;
            WarehouseId = cell.WarehouseId;
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int WarehouseId { get; set; }
        //public  WarehouseModel Warehouse { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public List<NomenclatureOfWarehouseModel> NomenclatureOfWarehouse { get; set; }
    }
}