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

        public async Task<IActionResult> Mesero()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Detalles)
                .Where(p => p.Estado != "ENTREGADO")
                .ToListAsync();
            return View(pedidos);
        }

        public async Task<IActionResult> CrearPedido()
        {
            // Cargar las mesas con sus pedidos asociados
            ViewBag.Mesas = await _context.Mesas.Include(m => m.Pedidos).ToListAsync();

            // Cargar los platos disponibles
            ViewBag.Platos = await _context.Platos.ToListAsync();

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

            ModelState.Remove("Facturas");
            if (ModelState.IsValid)
            {
                pedido.Fecha = DateTime.Now;
                pedido.Estado = "PENDIENTE";
                decimal total = 0;

                if (PlatosSeleccionados != null)
                {
                    foreach (var item in PlatosSeleccionados)
                    {
                        var platoId = item.Key;
                        var cantidad = item.Value;

                        var plato = await _context.Platos.FindAsync(platoId);
                        if (plato != null && cantidad > 0)
                        {
                            total += plato.Precio * cantidad;
                            pedido.Detalles.Add(new DetallePedido
                            {
                                Nombre = plato.Nombre,
                                Precio = plato.Precio,
                                Cantidad = cantidad,
                                Categoria = plato.Categoria
                            });
                        }
                    }
                }

                pedido.Total = total;

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

            pedido.Detalles.Add(new DetallePedido
            {
                Nombre = plato.Nombre,
                Precio = plato.Precio,
                Cantidad = cantidad,
                Categoria = plato.Categoria
            });


            pedido.Total += plato.Precio * cantidad;

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


            // Calcular la diferencia de cantidad
            var diferenciaCantidad = nuevaCantidad - detalle.Cantidad;

            // Actualizar el total del pedido
            pedido.Total += detalle.Precio * diferenciaCantidad;

            // Actualizar la cantidad del detalle
            detalle.Cantidad = nuevaCantidad;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DetallesPedido), new { id = idPedido });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MandarACocina(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = "LISTO PARA PREPARAR";
            _context.Update(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MandarACocinaMesero(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = "LISTO PARA PREPARAR";
            _context.Update(pedido);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Pedido en cocina exitosamente" });
        }


        public async Task<IActionResult> Cocina()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Detalles)
                .Where(p => p.Estado == "LISTO PARA PREPARAR")
                .ToListAsync();
            return View(pedidos);
        }

        public async Task<IActionResult> DetallesCocina(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarPedido(int idPedido)
        {
            var pedido = await _context.Pedidos.FindAsync(idPedido);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = "ORDEN LISTA";
            _context.Update(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Cocina));
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarPedido02(int idPedido)
        {
            var pedido = await _context.Pedidos.FindAsync(idPedido);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = "ORDEN LISTA";
            _context.Update(pedido);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Pedido finalizado exitosamente" });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EntregarAMesa(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Mesa)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = "ENTREGADO";
            pedido.Mesa.Estado = "DISPONIBLE";
            _context.Update(pedido);
            _context.Update(pedido.Mesa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EntregarAMesaMesero(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Mesa)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = "ENTREGADO";
            pedido.Mesa.Estado = "DISPONIBLE";
            _context.Update(pedido);
            _context.Update(pedido.Mesa);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Pedido Completado exitosamente" });
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

            detalle.Pedido.Total -= detalle.Precio * detalle.Cantidad;

            pedido.Detalles.Remove(detalle);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DetallesPedido), new { id = idPedido });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarDisponibilidad(int idPedido, List<int> detallesSeleccionados)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            foreach (var detalleId in detallesSeleccionados)
            {
                var detalle = pedido.Detalles.FirstOrDefault(d => d.Id == detalleId);
                if (detalle != null)
                {
                    detalle.ListaComida = true;
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DetallesCocina), new { id = idPedido });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstadoEntregado(int idPedido, List<int> detallesSeleccionados)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            foreach (var detalleId in detallesSeleccionados)
            {
                var detalle = pedido.Detalles.FirstOrDefault(d => d.Id == detalleId);
                if (detalle != null)
                {
                    detalle.Disponible = true; // Cambiamos el estado a "Entregado"
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DetallesPedido), new { id = idPedido });
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoEntregadoCocina(int idPedido, List<int> detallesSeleccionados)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            foreach (var detalleId in detallesSeleccionados)
            {
                var detalle = pedido.Detalles.FirstOrDefault(d => d.Id == detalleId);
                if (detalle != null)
                {
                    detalle.ListaComida = true; // Cambiamos el estado a "Entregado"
                }
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> CambiarEstadoEntregadoMesero(int idPedido, List<int> detallesSeleccionados)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            foreach (var detalleId in detallesSeleccionados)
            {
                var detalle = pedido.Detalles.FirstOrDefault(d => d.Id == detalleId);
                if (detalle != null)
                {
                    detalle.Disponible = true; // Cambiamos el estado a "Entregado"
                }
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public IActionResult ObtenerPedidosCocina()
        {
            var pedidos = _context.Pedidos
                                  .Include(p => p.Mesa)
                                  .Include(p => p.Detalles)
                                  .Where(p => p.Estado == "LISTO PARA PREPARAR")
                                  .Select(p => new
                                  {
                                      p.Id,
                                      Mesa = new { p.Mesa.NumMesa },
                                      p.Fecha,
                                      Total = p.Total.ToString("F2"),
                                      Detalles = p.Detalles.Select(d => new
                                      {
                                          d.Id,
                                          d.Nombre,
                                          d.Cantidad,
                                          d.ListaComida,
                                          d.Categoria,
                                      }).ToList()
                                  })
                                  .ToList();

            return Json(pedidos);
        }

        public IActionResult ObtenerPedidosMesero()
        {
            var pedidos = _context.Pedidos
                                  .Include(p => p.Mesa)
                                  .Include(p => p.Detalles)
                                  .Where(p => p.Estado != "ENTREGADO")
                                  .Select(p => new
                                  {
                                      p.Id,
                                      Mesa = new { p.Mesa.NumMesa },
                                      p.Fecha,
                                      Total = p.Total.ToString("F2"),
                                      p.Estado,
                                      Detalles = p.Detalles.Select(d => new
                                      {
                                          d.Id,
                                          d.Nombre,
                                          d.Cantidad,
                                          d.ListaComida,
                                          d.Categoria,
                                          d.Disponible,
                                      }).ToList()
                                  })
                                  .ToList();

            return Json(pedidos);
        }

        [HttpGet]
        public JsonResult ObtenerDetallesPedido(int idPedido)
        {
            var pedido = _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefault(p => p.Id == idPedido);

            if (pedido == null)
            {
                return Json(new { success = false, message = "Pedido no encontrado." });
            }

            var detallesPendientes = pedido.Detalles
                .Where(d => d.Categoria == "Comida" && !d.ListaComida)
                .Select(d => new
                {
                    d.Id,
                    d.Nombre,
                    d.Cantidad
                }).ToList();

            return Json(new { success = true, detallesPendientes });
        }






    }
}
