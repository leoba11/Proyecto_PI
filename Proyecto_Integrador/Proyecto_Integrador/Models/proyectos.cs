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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class proyectos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proyectos()
        {
            this.modulos = new HashSet<modulos>();
            this.roles = new HashSet<roles>();
        }

        [Key]
        [Display(Name = "C�digo del Proyecto")]
        public int codigoPK { get; set; }
        [Display(Name = "Nombre del Proyecto")]
        public string nombre { get; set; }
        [Display(Name = "Fecha de inicio")]
        public System.DateTime fechaInicio { get; set; }
        [Display(Name = "Fecha final estimada")]
        public System.DateTime fechaFinalEstimada { get; set; }
        [Display(Name = "Costo estimado")]
        public decimal costoEstimado { get; set; }
        [Display(Name = "Objetivo del Proyecto")]
        public string objetivo { get; set; }
        public string cedulaClienteFK { get; set; }
        [Display(Name = "ID de equipo")]
        public Nullable<int> idEquipo { get; set; }
        [Display(Name = "Fecha final")]
        public Nullable<System.DateTime> fechaFinal { get; set; }
        [Display(Name = "Costo real")]
        public Nullable<decimal> costoReal { get; set; }

        public virtual clientes clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<modulos> modulos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<roles> roles { get; set; }
    }
}
