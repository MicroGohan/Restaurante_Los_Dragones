using Bar_Restaurante_Los_Dragones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class HomeController : Controller
    {

        private readonly ProyectoContext _context;

        public HomeController(ProyectoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> home()
        {
            int cantidadFacturas = await _context.Facturas.CountAsync();

            int cantidadComidas = await _context.Platos.CountAsync();

            int cantidadUsuarios = await _context.Usuarios.CountAsync();

            int cantidad = await _context.Platos.CountAsync();

            // Pasa la cantidad de facturas a la vista
            ViewBag.CantidadFacturas = cantidadFacturas;

            ViewBag.CantidadComidas = cantidadComidas;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
