//------------------------------------------------------------------------------
// @author: Maria Jose Aguilar 
// @date : 2/10/19   
// OJO! Para poder desplegar en las vistas un nombre diferente que el nombre dado en la base de datos se utiliza  [Display(Name = "nombre que quiere desplegar")]
// Para poder usar display, incluir -> using System.ComponentModel.DataAnnotations; 
//  Se coloca el required para que se maneje la excepción de que el usuario no introduzca el valor.    
// 
//------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace Proyecto_Integrador.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
