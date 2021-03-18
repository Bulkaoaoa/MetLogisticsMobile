using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerumDesktop.Entities
{
    public partial class StepOfOrder
    {
        public string NumenclatureName
        {
            get
            {
                return Shipment.NomenclatureOfWarehouse.Nomenclature.Name;
            }
        }
    }
}
