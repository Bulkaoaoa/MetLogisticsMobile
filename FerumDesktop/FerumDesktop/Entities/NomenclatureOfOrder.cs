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
    
    public partial class NomenclatureOfOrder
    {
        public string NumenclatureId { get; set; }
        public string OrderId { get; set; }
        public decimal Count { get; set; }
    
        public virtual Nomenclature Nomenclature { get; set; }
        public virtual Order Order { get; set; }
    }
}
