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
        public string cedulaPK { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El nombre solo puede estar compuesto por letras")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Los Apellido solo puede estar compuesto por letras")]
        [Display(Name = "Primer Apellido")]
        public string apellido1 { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Los Apellido solo puede estar compuesto por letras")]
        [Display(Name = "Segundo Apellido")]
        public string apellido2 { get; set; }

        [StringLength(15)]
        [Display(Name = "Tel�fono")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "El n�mero de telefono solo �ede estar compuesto por n�meros")]
        public string telefono { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "La provincia solo puede estar compuesto por letras")]
        [Display(Name = "Provincia")]
        public string provincia { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El Cant�n solo puede estar compuesto por letras")]
        [Display(Name = "Cant�n")]
        public string canton { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El Distrito solo puede estar compuesto por letras")]
        [Display(Name = "Distrito")]
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
