using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class NomenclatureOfOrderModel
    {
        public NomenclatureOfOrderModel(NomenclatureOfOrder nomenclatureOfOrder, bool viewOrder)
        {
            Nomenclature = new NomenclatureModel(nomenclatureOfOrder.Nomenclature, false, true);
            if (viewOrder)
                Order = new OrderModel(nomenclatureOfOrder.Order, false, false);
            Count = nomenclatureOfOrder.Count;
        }
        [JsonIgnore]
        public string NumenclatureId { get; set; }
        [JsonIgnore]
        public string OrderId { get; set; }
        public decimal Count { get; set; }

        public NomenclatureModel Nomenclature { get; set; }
        [JsonIgnore]
        public virtual OrderModel Order { get; set; }
    }
}