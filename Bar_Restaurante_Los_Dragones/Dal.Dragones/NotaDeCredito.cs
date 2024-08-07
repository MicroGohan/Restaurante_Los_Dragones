using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Dragones
{
    public class NotaDeCredito
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int Monto { get; set; }
        public DateTime Fecha { get; set; }

        public Factura Factura { get; set; }
    }
}
