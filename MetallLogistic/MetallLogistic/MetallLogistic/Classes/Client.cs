using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> Login { get; set; }
        public Nullable<System.Guid> Password { get; set; }
    }
}
