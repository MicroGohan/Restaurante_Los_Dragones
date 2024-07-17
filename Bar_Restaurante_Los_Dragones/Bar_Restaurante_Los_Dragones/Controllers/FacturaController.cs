using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bar_Restaurante_Los_Dragones.Models;
using Dal.Dragones;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class FacturaController : Controller
    {
        private readonly ProyectoContext _context;

        public FacturaController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Factura
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Facturas.Include(f => f.Pedidos);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Factura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var factura = await _context.Facturas
                .Include(f => f.Pedidos)
                .FirstOrDefaultAsync(m => m.id == id);
            if (factura == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == factura.PedidoId);
            
            factura.Pedidos = pedido;

            return View(factura);
        }


        // POST: Factura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Fecha,NombreCliente,Responsable,Subtotal,Descuento,Iva,TotalPagar,Observaciones,MetodoPago,PedidoId")] Factura factura)
        {
            factura.id = 0;
            ModelState.Remove("Pedidos");
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MetodoPagoList = new SelectList(Enum.GetValues(typeof(MetodoPago)).Cast<MetodoPago>());
            return View(factura);
        }

        public async Task<IActionResult> Create(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            ViewBag.MetodoPagoList = new SelectList(Enum.GetValues(typeof(MetodoPago)).Cast<MetodoPago>());
            var pedido = await _context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }
            var factura = new Dal.Dragones.Factura
            {

                PedidoId = pedido.Id,
                Fecha = DateTime.Now,
                Subtotal = pedido.Total,
                Iva = 13, 
                Pedidos = pedido
            };


            return View(factura);
        }

        // GET: Factura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewBag.MetodoPagoList = new SelectList(Enum.GetValues(typeof(MetodoPago)).Cast<MetodoPago>());
            return View(factura);
        }

        // POST: Factura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Fecha,NombreCliente,Responsable,Subtotal,Descuento,Iva,TotalPagar,Observaciones,MetodoPago,PedidoId")] Factura factura)
        {
            if (id != factura.id)
            {
                return NotFound();
            }
            ModelState.Remove("Pedidos");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MetodoPagoList = new SelectList(Enum.GetValues(typeof(MetodoPago)).Cast<MetodoPago>());
            return View(factura);
        }

        // GET: Factura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Pedidos)
                .FirstOrDefaultAsync(m => m.id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.id == id);
        }
    }
}
