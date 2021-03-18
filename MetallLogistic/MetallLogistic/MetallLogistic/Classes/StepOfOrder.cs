using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
    public class StepOfOrder
    {
        public int Id { get; set; }
        public bool isDone { get; set; }
        public Nullable<int> TimeSpent { get; set; }
        public Nullable<System.DateTime> DateOfStart { get; set; }
        public Nullable<System.DateTime> DateOfEnd { get; set; }
        public Movement Movement { get; set; }
        public Order Order { get; set; }
        public Proccess Proccess { get; set; }
        public Shipment Shipment { get; set; }
        public List<Trouble> Trouble
        {
            get; set;
        }
    }
}
