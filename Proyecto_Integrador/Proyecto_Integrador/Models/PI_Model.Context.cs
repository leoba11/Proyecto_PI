//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Integrador.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class Gr02Proy1Entities : DbContext
    {
        public Gr02Proy1Entities()
            : base("name=Gr02Proy1Entities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<clientes> clientes { get; set; }
        public virtual DbSet<conocimientos> conocimientos { get; set; }
        public virtual DbSet<empleados> empleados { get; set; }
        public virtual DbSet<modulos> modulos { get; set; }
        public virtual DbSet<proyectos> proyectos { get; set; }
        public virtual DbSet<requerimientos> requerimientos { get; set; }
        public virtual DbSet<roles> roles { get; set; }
    }
}