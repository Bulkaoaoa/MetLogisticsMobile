//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FerumDesktop.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Queue
    {
        public string OrderId { get; set; }
        public Nullable<int> WarehosueId { get; set; }
        public int Id { get; set; }
        public int Place { get; set; }
        public int TypeOfQueue { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual TypeOfQueue TypeOfQueue1 { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
