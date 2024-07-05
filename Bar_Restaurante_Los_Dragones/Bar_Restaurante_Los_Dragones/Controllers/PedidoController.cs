using Bar_Restaurante_Los_Dragones.Models;
using Dal.Dragones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ProyectoContext _context;

        public PedidoController(ProyectoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos.Include(p => p.Mesa).ToListAsync();
            return View(pedidos);
        }

        public async Task<IActionResult> CrearPedido()
        {
            ViewBag.Mesas = await _context.Mesas.ToListAsync();
            ViewBag.Platos = _context.Platos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearPedido(Pedido pedido, Dictionary<int, int> PlatosSeleccionados)
        {
            ModelState.Remove("Total");
            ModelState.Remove("Fecha");
            ModelState.Remove("Estado");
            ModelState.Remove("Mesa");
            ModelState.Remove("Detalles");
            if (ModelState.IsValid)
            {
                pedido.Fecha = DateTime.Now;
                pedido.Estado = "PENDIENTE";

                if (PlatosSeleccionados != null)
                {
                    foreach (var item in PlatosSeleccionados)
                    {
                        var platoId = item.Key;
                        var cantidad = item.Value;

                        var plato = await _context.Platos.FindAsync(platoId);
                        if (plato != null && cantidad > 0)
                        {
                            pedido.Detalles.Add(new DetallePedido
                            {
                                Nombre = plato.Nombre,
                                Precio = plato.Precio,
                                Cantidad = cantidad,
                            });
                        }
                    }
                }

                // Marcar la mesa como ocupada
                var mesa = await _context.Mesas.FindAsync(pedido.IdMesa);
                if (mesa != null)
                {
                    mesa.Estado = "OCUPADA";
                    _context.Update(mesa);
                }

                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mesas = await _context.Mesas.ToListAsync();
            ViewBag.Platos = await _context.Platos.ToListAsync();
            return View(pedido);
        }






        public async Task<IActionResult> DetallesPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewBag.Platos = await _context.Platos.Where(p => p.Disponible).ToListAsync();
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AñadirPlato(int idPedido, int idPlato, int cantidad)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos.FindAsync(idPlato);
            if (plato == null)
            {
                return NotFound();
            }

            // Verificar si el plato ya existe en el detalle del pedido por su nombre
            var detalleExistente = pedido.Detalles.FirstOrDefault(d => d.Nombre == plato.Nombre);
            if (detalleExistente != null)
            {
                // Si existe, simplemente aumentamos la cantidad
                detalleExistente.Cantidad += cantidad;
            }
            else
            {
                // Si no existe, lo añadimos como un nuevo detalle
                pedido.Detalles.Add(new DetallePedido
                {
                    Nombre = plato.Nombre,
                    Precio = plato.Precio,
                    Cantidad = cantidad
                });
            }

            await _context.SaveChangesAsync();

            // Redirigir de vuelta a la vista de detalles del pedido
            return RedirectToAction(nameof(DetallesPedido), new { id = idPedido });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarCantidadPlatillo(int idPedido, int idDetalle, int nuevaCantidad)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            var detalle = pedido.Detalles.FirstOrDefault(d => d.Id == idDetalle);
            if (detalle == null)
            {
                return NotFound();
            }

            detalle.Cantidad = nuevaCantidad;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DetallesPedido), new { id = idPedido });
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas.FindAsync(pedido.IdMesa);
            if (mesa != null)
            {
                mesa.Estado = "Disponible";
                _context.Update(mesa);
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPlatillo(int idPedido, int idDetalle)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            var detalle = pedido.Detalles.FirstOrDefault(d => d.Id == idDetalle);
            if (detalle == null)
            {
                return NotFound();
            }

            pedido.Detalles.Remove(detalle);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DetallesPedido), new { id = idPedido });
        }


    }
}
