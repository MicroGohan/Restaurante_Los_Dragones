using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Dragones
{
    public class Factura
    {
        [Key]
        public int id { get; set; }
        public DateTime Fecha { get; set; }
        public string? NombreCliente { get; set; }
        public string Responsable { get; set; }
        public decimal Subtotal { get; set; }
        public int? Descuento { get; set; }
        public int Iva { get; set; }
        public decimal TotalPagar { get; set; }
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "El campo Metodo de Pago es obligatorio")]
        [Display(Name = "Metodo de Pago")]
        public MetodoPago MetodoPago { get; set; }

        public int PedidoId { get; set; }
        public virtual Pedido Pedidos { get; set; }

    }
    public enum MetodoPago
    {
        Efectivo,
        Tarjeta,
        Sinpe
    }
}
