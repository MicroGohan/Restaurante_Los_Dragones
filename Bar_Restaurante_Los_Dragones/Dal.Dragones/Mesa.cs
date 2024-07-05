using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Dragones
{
    public class Mesa
    {
        public int Id { get; set; }
        public int NumMesa { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
