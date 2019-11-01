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

    public partial class requerimientos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [Key]
        [Display(Name = "Proyecto")]
        public int codigoProyectoFK { get; set; }
        [Key]
        [Display(Name = "Módulo")]
        public int idModuloFK { get; set; }
        [Key]
        public int idPK { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string descripcion { get; set; }

        [RegularExpression(@"^[0-9]+", ErrorMessage = "Complejidad es un número")]
        [Display(Name = "Complejidad")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int complejidad { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string estado { get; set; }

        [Display(Name = "Empleado")]
        public string cedulaEmpleadoFK { get; set; }


        [Display(Name = "Fecha de inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // para que la fecha en editar muestre el valor
        [Required(ErrorMessage = "Este campo es requerido")]
        public System.DateTime fechaInicio { get; set; }

        [Display(Name = "Fecha de finalización ")]
        public Nullable<System.DateTime> fechaFin { get; set; }

        [Display(Name = "Fecha estimada de finalización")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // para que la fecha en editar muestre el valor
        [Required(ErrorMessage = "Este campo es requerido")]
        public System.DateTime duraciónEstimada { get; set; }

        [Display(Name = "Fecha de finalización real")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // para que la fecha en editar muestre el valor
        public Nullable<System.DateTime> duraciónReal { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Requerimiento")]
        public string nombre { get; set; }
    
        public virtual empleados empleados { get; set; }
        public virtual modulos modulos { get; set; }
    }
}
