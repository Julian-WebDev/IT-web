﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IT_WEB.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IT_Assets_Entities : DbContext
    {
        public IT_Assets_Entities()
            : base("name=IT_Assets_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CR_IT_COMBOS> CR_IT_COMBOS { get; set; }
        public virtual DbSet<T_CATEGORY> T_CATEGORY { get; set; }
        public virtual DbSet<T_ADMINS> T_ADMINS { get; set; }
        public virtual DbSet<ATL_IT_ASSETS> ATL_IT_ASSETS { get; set; }
        public virtual DbSet<CR_IT_ASSETS> CR_IT_ASSETS { get; set; }
        public virtual DbSet<PNQ_IT_ASSETS> PNQ_IT_ASSETS { get; set; }
        public virtual DbSet<T_IT_ASSETS> T_IT_ASSETS { get; set; }
    }
}
