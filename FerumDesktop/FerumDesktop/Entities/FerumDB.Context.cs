﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FerumEntities : DbContext
    {
        public FerumEntities()
            : base("name=FerumEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cell> Cell { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Courier> Courier { get; set; }
        public virtual DbSet<Movement> Movement { get; set; }
        public virtual DbSet<MovementStorekeeper> MovementStorekeeper { get; set; }
        public virtual DbSet<Nomenclature> Nomenclature { get; set; }
        public virtual DbSet<NomenclatureOfOrder> NomenclatureOfOrder { get; set; }
        public virtual DbSet<NomenclatureOfWarehouse> NomenclatureOfWarehouse { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Proccess> Proccess { get; set; }
        public virtual DbSet<Queue> Queue { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<StepOfOrder> StepOfOrder { get; set; }
        public virtual DbSet<Storekeeper> Storekeeper { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Trouble> Trouble { get; set; }
        public virtual DbSet<TypeOfQueue> TypeOfQueue { get; set; }
        public virtual DbSet<TypeOfTrouble> TypeOfTrouble { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
    }
}
