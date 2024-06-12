using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [DisplayName("Correo Electronico")]
        [Required(ErrorMessage = "El campo Correo Electronico es obligatorio")]
        [MaxLength(100)]
        [EmailAddress]
        public string Correo { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        [DisplayName("Digite la confirmacion de la contraseña")]
        [Required(ErrorMessage = "El campo Contraseña Confirmar es obligatorio")]
        [NotMapped] // Esta propiedad no se mapeará a la base de datos
        [Compare("Clave", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; }

        [Required(ErrorMessage = "El campo Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public string[] Roles { get; set; }
    }
}
