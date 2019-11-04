﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoIntegrador_mejorado.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    
        public virtual ObjectResult<conocimientos_en_rango_Result> conocimientos_en_rango(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFinal)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinalParameter = fechaFinal.HasValue ?
                new ObjectParameter("fechaFinal", fechaFinal) :
                new ObjectParameter("fechaFinal", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<conocimientos_en_rango_Result>("conocimientos_en_rango", fechaInicioParameter, fechaFinalParameter);
        }
    
        public virtual ObjectResult<EmpleadosDesocupados_Result> EmpleadosDesocupados(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("fecha1", fecha1) :
                new ObjectParameter("fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("fecha2", fecha2) :
                new ObjectParameter("fecha2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmpleadosDesocupados_Result>("EmpleadosDesocupados", fecha1Parameter, fecha2Parameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<empleados> EmpleadosParaReporteFechas(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("fecha1", fecha1) :
                new ObjectParameter("fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("fecha2", fecha2) :
                new ObjectParameter("fecha2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<empleados>("EmpleadosParaReporteFechas", fecha1Parameter, fecha2Parameter);
        }
    
        public virtual ObjectResult<empleados> EmpleadosParaReporteFechas(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, MergeOption mergeOption)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("fecha1", fecha1) :
                new ObjectParameter("fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("fecha2", fecha2) :
                new ObjectParameter("fecha2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<empleados>("EmpleadosParaReporteFechas", mergeOption, fecha1Parameter, fecha2Parameter);
        }
    
        public virtual ObjectResult<ObtenerRequerimientos_Result> ObtenerRequerimientos(Nullable<int> idProy, string cedulaEmp)
        {
            var idProyParameter = idProy.HasValue ?
                new ObjectParameter("idProy", idProy) :
                new ObjectParameter("idProy", typeof(int));
    
            var cedulaEmpParameter = cedulaEmp != null ?
                new ObjectParameter("cedulaEmp", cedulaEmp) :
                new ObjectParameter("cedulaEmp", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerRequerimientos_Result>("ObtenerRequerimientos", idProyParameter, cedulaEmpParameter);
        }
    
        public virtual ObjectResult<requerimientos> cantidadRequerimientos(Nullable<int> idProy, string cedulaEmp)
        {
            var idProyParameter = idProy.HasValue ?
                new ObjectParameter("idProy", idProy) :
                new ObjectParameter("idProy", typeof(int));
    
            var cedulaEmpParameter = cedulaEmp != null ?
                new ObjectParameter("cedulaEmp", cedulaEmp) :
                new ObjectParameter("cedulaEmp", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<requerimientos>("cantidadRequerimientos", idProyParameter, cedulaEmpParameter);
        }
    
        public virtual ObjectResult<requerimientos> cantidadRequerimientos(Nullable<int> idProy, string cedulaEmp, MergeOption mergeOption)
        {
            var idProyParameter = idProy.HasValue ?
                new ObjectParameter("idProy", idProy) :
                new ObjectParameter("idProy", typeof(int));
    
            var cedulaEmpParameter = cedulaEmp != null ?
                new ObjectParameter("cedulaEmp", cedulaEmp) :
                new ObjectParameter("cedulaEmp", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<requerimientos>("cantidadRequerimientos", mergeOption, idProyParameter, cedulaEmpParameter);
        }
    }
}
