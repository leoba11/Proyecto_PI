//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoIntegrador_mejorado.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class modulos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public modulos()
        {
            this.requerimientos = new HashSet<requerimientos>();
        }

        [Key]
        [Display(Name = "Nombre del Proyecto")]
        public int codigoProyectoFK { get; set; }
        [Key]
        public int idPK { get; set; }

        [Display(Name = "Nombre del módulo")]
        [RegularExpression(@"^[A-Za-z0-9\.\ ,]+", ErrorMessage = "texto contiene caracteres no permitidos")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string descripcion { get; set; }

        public virtual proyectos proyectos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<requerimientos> requerimientos { get; set; }
    }
}
