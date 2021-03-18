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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.NomenclatureOfOrder = new HashSet<NomenclatureOfOrder>();
            this.Queue = new HashSet<Queue>();
            this.StepOfOrder = new HashSet<StepOfOrder>();
        }
    
        public string Id { get; set; }
        public System.DateTime DateOfCreate { get; set; }
        public string ClientId { get; set; }
        public Nullable<int> CourierId { get; set; }
        public Nullable<decimal> Effectiveness { get; set; }
        public Nullable<System.DateTime> DateTimeOfArrivle { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Courier Courier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NomenclatureOfOrder> NomenclatureOfOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StepOfOrder> StepOfOrder { get; set; }
    }
}
