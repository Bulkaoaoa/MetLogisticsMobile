using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
   public  class Courier
    {
        public int Id { get; set; }
        public string TelephoneNumber { get; set; }
        public string FIO { get; set; }
        public string CarNumber { get; set; }
        public decimal CarCarrying { get; set; }
        public System.Guid Password { get; set; }
        public string ClientId { get; set; }

        public Client Client { get; set; }
    }
}
