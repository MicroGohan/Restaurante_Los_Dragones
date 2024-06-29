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
    public class UsuarioController : Controller
    {
        private readonly ProyectoContext _context;

        public UsuarioController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Usuarios.Include(u => u.Rol);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            ViewData["RolID"] = new SelectList(_context.Roles, "ID", "Nombre");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Correo,Clave,RolID")] Usuario usuario)
        {
            ModelState.Remove("ConfirmarClave");
            ModelState.Remove("Rol");
            if (ModelState.IsValid)
            {
                // Verificar si el correo electrónico ya está registrado
                if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
                {
                    ModelState.AddModelError("Correo", "El correo electrónico ya está registrado.");
                    ViewData["RolID"] = new SelectList(_context.Roles, "ID", "Nombre");
                    return View(usuario);
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("AdministradorListado", "Administracion");
            }
            ViewData["RolID"] = new SelectList(_context.Roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["RolID"] = new SelectList(_context.Roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Correo,Clave,RolID")] Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return NotFound();
            }
            ModelState.Remove("ConfirmarClave");
            ModelState.Remove("Rol");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.ID))
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
            ViewData["RolID"] = new SelectList(_context.Roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}
