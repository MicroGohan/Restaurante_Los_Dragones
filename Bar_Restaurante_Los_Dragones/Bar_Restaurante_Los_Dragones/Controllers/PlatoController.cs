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
    public class PlatoController : Controller
    {
        private readonly ProyectoContext _context;

        public PlatoController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Plato
        public async Task<IActionResult> Index()
        {
            return View(await _context.Platos.ToListAsync());
        }

        // GET: Plato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: Plato/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,ImagenData,Disponible,Categoria")] Plato plato, IFormFile imagenArchivo)
        {
            ModelState.Remove("ImagenData");
            if (ModelState.IsValid)
            {
                if (imagenArchivo != null && imagenArchivo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagenArchivo.CopyToAsync(memoryStream);
                        plato.ImagenData = memoryStream.ToArray();
                    }
                }

                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plato);
        }

        // GET: Plato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return View(plato);
        }

        // POST: Plato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,ImagenData,Disponible,Categoria")] Plato plato, IFormFile ImagenData)
        {
            if (id != plato.Id)
            {
                return NotFound();
            }
            ModelState.Remove("ImagenData");
            if (ModelState.IsValid)
            {
                try
                {
                    if (ImagenData != null && ImagenData.Length > 0)
                    {
                        // Si se cargó una nueva imagen, asignar los datos de la nueva imagen al auto
                        using (var memoryStream = new MemoryStream())
                        {
                            await ImagenData.CopyToAsync(memoryStream);
                            plato.ImagenData = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        var existingPlato = await _context.Platos.AsNoTracking().FirstOrDefaultAsync(z => z.Id == plato.Id);
                        if (existingPlato != null)
                        {
                            plato.ImagenData = existingPlato.ImagenData;
                        }
                    }

                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(plato.Id))
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
            return View(plato);
        }

        // GET: Plato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: Plato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plato = await _context.Platos.FindAsync(id);
            if (plato != null)
            {
                _context.Platos.Remove(plato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoExists(int id)
        {
            return _context.Platos.Any(e => e.Id == id);
        }
    }
}