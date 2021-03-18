using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
   public  class NomenclatureOfWarehouse
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public virtual Cell Cell { get; set; }
        public virtual Nomenclature Nomenclature { get; set; }
    }
}
