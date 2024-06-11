using System.ComponentModel.DataAnnotations;

namespace Bar_Restaurante_Los_Dragones.Models.ViewModels
{
    public class UserControllerViewModel
    {
        [Required(ErrorMessage = "El campo de nombre es requerido.")]
        [MaxLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [MaxLength(100)]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [MaxLength(100)]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Display(Name = "Rol")]
        public string IdRol { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe ser como mínimo {2} y máximo {1} carácteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña de Confirmación")]
        [Compare("Password", ErrorMessage = "La contraseña no coincide con la contraseña de confirmación.")]
        public string ConfirmPassword { get; set; }
    }
}
