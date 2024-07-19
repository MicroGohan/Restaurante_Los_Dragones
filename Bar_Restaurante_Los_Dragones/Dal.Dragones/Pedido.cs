using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Dragones
{
    public class Pedido
    {
        public Pedido()
        {
            Detalles = new List<DetallePedido>();
        }

        public int Id { get; set; }
        public int IdMesa { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }

        public virtual Mesa Mesa { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<DetallePedido> Detalles { get; set; }
    }
}
