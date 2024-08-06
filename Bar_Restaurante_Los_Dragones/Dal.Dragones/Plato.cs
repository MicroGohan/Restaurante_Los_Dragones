using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Dragones
{
    public class Plato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }

        [Display(Name = "Imagen")]
        public byte[]? ImagenData { get; set; }

        [Required(ErrorMessage = "El campo Disponible es obligatorio")]
        [Display(Name = "Disponible")]
        public bool Disponible { get; set; }

        public string Categoria { get; set; }
    }

}
