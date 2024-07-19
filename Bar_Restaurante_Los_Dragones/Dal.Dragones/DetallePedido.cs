using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Dragones
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Disponible es obligatorio")]
        [Display(Name = "Disponible")]
        public bool Disponible { get; set; }
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El campo ListaComida es obligatorio")]
        [Display(Name = "ListaComida")]
        public bool ListaComida { get; set; }

        public string Categoria { get; set; }

        public virtual Pedido Pedido { get; set; }
    }
}
