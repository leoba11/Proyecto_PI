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

    /*Cada atributo de Cliente esta respectivamente limitado con una expresion regular de forma que el dato ingresado por el usuario no cause problemas al sistema.
    *Provincia, canton y distrito no poseen ER porque se seleccionan del DropDownList y la direccion detallada no posee restriccion con ER por evidente motivo.
    */
    public partial class clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clientes()
        {
            this.proyectos = new HashSet<proyectos>();
        }
        [Key]
        [StringLength(9)]
        [RegularExpression(@"^[0-9]+", ErrorMessage = "La c�dula solo puede estar compuesta por n�meros")]
        [Display(Name = "C�dula")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string cedulaPK { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El nombre solo puede estar compuesto por letras")]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string nombre { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Los Apellido solo puede estar compuesto por letras")]
        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string apellido1 { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Los Apellido solo puede estar compuesto por letras")]
        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string apellido2 { get; set; }

        [StringLength(15)]
        [Display(Name = "Tel�fono")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "El n�mero de telefono solo �ede estar compuesto por n�meros,y no puede iniciar con el numero 0")]
        public string telefono { get; set; }

        [StringLength(20)]
        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string provincia { get; set; }

        [StringLength(20)]
        [Display(Name = "Cant�n")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string canton { get; set; }

        [StringLength(20)]
        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string distrito { get; set; }

        [StringLength(25)]
        [Display(Name = "E-mail")]
        public string correo { get; set; }

        [Display(Name = "Direccion detallada")]
        public string direccionDetallada { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proyectos> proyectos { get; set; }
    }
}
