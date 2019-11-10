//------------------------------------------------------------------------------
// @author: María José Aguilar Barboza B60115
// @Date: 09/11/19
// Este código corresponde al modelo de la tabla de módulos de la base de datos.
// Se hace uso de DataAnnotations para hacer que en las vistas se desplieguen los valores deseados.  
// Asimismo, se utilizan expresiones regulares para manejar la entrada del usuario. 
// Este código en su mayoría fue autogenerado.
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
