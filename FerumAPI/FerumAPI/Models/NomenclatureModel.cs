using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class NomenclatureModel
    {
        public NomenclatureModel(Nomenclature nomenclature)
        {
            Id = nomenclature.Id;
            Name = nomenclature.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //[JsonIgnore]
        //public List<NomenclatureOfOrderModel> NomenclatureOfOrder { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //[JsonIgnore]
        //public List<NomenclatureOfWarehouseModel> NomenclatureOfWarehouse { get; set; }
    }
}