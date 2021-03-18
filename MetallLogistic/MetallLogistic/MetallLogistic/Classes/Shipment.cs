using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
   public class Shipment
    {
        public int Id { get; set; }
        public decimal Count { get; set; }

        public NomenclatureOfWarehouse NomenclatureOfWarehouse { get; set; }
       
    }
}
