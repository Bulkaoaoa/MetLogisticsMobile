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
        public NomenclatureModel(Nomenclature nomenclature, bool viewNomenclatureOfOrder, bool viewNomenclatureOfWaregouse)
        {
            Id = nomenclature.Id;
            Name = nomenclature.Name;
            if (viewNomenclatureOfOrder)
                NomenclatureOfOrder = nomenclature.NomenclatureOfOrder.ToList().ConvertAll(p=>new NomenclatureOfOrderModel(p, false));
            if (viewNomenclatureOfWaregouse)
                NomenclatureOfWarehouse = nomenclature.NomenclatureOfWarehouse.ToList().ConvertAll(p => new NomenclatureOfWarehouseModel(p,false));
        }

        public string Id { get; set; }
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public List<NomenclatureOfOrderModel> NomenclatureOfOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public List<NomenclatureOfWarehouseModel> NomenclatureOfWarehouse { get; set; }
    }
}