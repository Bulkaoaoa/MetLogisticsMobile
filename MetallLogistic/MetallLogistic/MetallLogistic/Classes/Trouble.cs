using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
    public class Trouble
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProccesId { get; set; }

        public virtual TypeOfTrouble TypeOfTrouble { get; set; }
    }
}
