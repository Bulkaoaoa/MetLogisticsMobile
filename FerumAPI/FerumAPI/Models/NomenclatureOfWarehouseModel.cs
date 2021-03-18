using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class NomenclatureOfWarehouseModel
    {
        public NomenclatureOfWarehouseModel(NomenclatureOfWarehouse nomenclatureModel)
        {
            Id = nomenclatureModel.Id;
            Nomenclature = new NomenclatureModel(nomenclatureModel.Nomenclature);
            //Count = nomenclatureModel.Count;
            Barcode = nomenclatureModel.Barcode;
            Cell = new CellModel(nomenclatureModel.Cell);
        }
        [JsonIgnore]
        public string NomenclatureId { get; set; }
        //public decimal Count { get; set; }
        public int Id { get; set; }
        public string Barcode { get; set; }
        [JsonIgnore]
        public int CellId { get; set; }
        public virtual CellModel Cell { get; set; }
        public virtual NomenclatureModel Nomenclature { get; set; }
    }
}