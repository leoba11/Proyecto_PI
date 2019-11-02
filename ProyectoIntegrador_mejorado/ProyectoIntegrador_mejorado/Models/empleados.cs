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
    public partial class empleados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public empleados()
        {
            this.conocimientos = new HashSet<conocimientos>();
            this.requerimientos = new HashSet<requerimientos>();
            this.roles = new HashSet<roles>();
        }

        [Key]
        [Display(Name = "Cédula")]
        [StringLength(9)]
        [RegularExpression(@"^[0-9]+", ErrorMessage = "La cédula solo puede estar compuesta por números")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string cedulaPK { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El nombre solo puede estar compuesto por letras")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string nombre { get; set; }

        [Display(Name = "Primer Apellido")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Los apellidos solo pueden estar compuesto por letras")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string apellido1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Los apellidos solo pueden estar compuesto por letras")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string apellido2 { get; set; }

        [Display(Name = "Edad")]
        public Nullable<int> edad { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // para que la fecha en editar muestre el valor
        public System.DateTime fechaNacimiento { get; set; }

        [Display(Name = "Telefono")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Este espacio solo debe tener números")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string telefono { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string provincia { get; set; }

        [Display(Name = "Cantón")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string canton { get; set; }

        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string distrito { get; set; }

        [StringLength(25)]
        [Display(Name = "Correo")]
        public string correo { get; set; }


        [Display(Name = "Dirección Completa")]
        public string direccionDetallada { get; set; }

        [Display(Name = "Disponibilidad")]
        public bool disponibilidad { get; set; }

        public Nullable<int> cantidadRequerimientos { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conocimientos> conocimientos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<requerimientos> requerimientos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<roles> roles { get; set; }
    }
}
