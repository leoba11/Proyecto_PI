//------------------------------------------------------------------------------
// @author: María José Aguilar Barboza B60115
// @Date: 09/11/19
// Este código corresponde al modelo de la tabla de requerimientos de la base de datos.
// Se hace uso de DataAnnotations para hacer que en las vistas se desplieguen los valores deseados.  
// Asimismo, se utilizan expresiones regulares para manejar la entrada del usuario. 
// Este código en su mayoría fue autogenerado.
//------------------------------------------------------------------------------

namespace ProyectoIntegrador_mejorado.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
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

        [RegularExpression(@"^[0-4]+", ErrorMessage = "Complejidad es un número del 1 al 4")]
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // para que la fecha en editar muestre el valor
        public Nullable<System.DateTime> fechaFin { get; set; }

        [Display(Name = "Fecha estimada de finalización")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // para que la fecha en editar muestre el valor
        [Required(ErrorMessage = "Este campo es requerido")]
        public System.DateTime duracionEstimada { get; set; }

       

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Requerimiento")]
        public string nombre { get; set; }

        [Display(Name = "Días empleados")]
        public Nullable<int> duracionDias { get; set; }


        // Esta propiedad va a contener las diferentes opciones de valores de estados que puede tener un requerimiento
        public IEnumerable<SelectListItem> estados { get; set; }

        public virtual empleados empleados { get; set; }
        public virtual modulos modulos { get; set; }
        public virtual proyectos proyectos { get; set; }
    }
}
