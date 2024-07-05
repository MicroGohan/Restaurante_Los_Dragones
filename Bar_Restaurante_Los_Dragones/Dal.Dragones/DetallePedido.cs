using System;
using System.Collections.Generic;
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
        public int IdPedido { get; set; }

        public virtual Pedido Pedido { get; set; }
    }
}
