using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Dragones
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [DisplayName("Correo Electrónico")]
        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es una dirección válida")]
        [MaxLength(100)]
        public string Correo { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [MaxLength(100)]
        [PasswordComplexity]
        public string Clave { get; set; }

        [DisplayName("Digite la confirmación de la contraseña")]
        [Required(ErrorMessage = "El campo Contraseña Confirmar es obligatorio")]
        [NotMapped] // Esta propiedad no se mapeará a la base de datos
        [Compare("Clave", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; }

        [Required(ErrorMessage = "El campo Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public int RolID { get; set; }

        [ForeignKey("RolID")]
        public Rol Rol { get; set; }
    }
}
