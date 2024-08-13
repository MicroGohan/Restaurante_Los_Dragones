using Bar_Restaurante_Los_Dragones.Models;
using Dal.Dragones;
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

            int cantidadOrdenes = await _context.Pedidos.CountAsync();

            int cantidadRoles = await _context.Roles.CountAsync();

            int cantidadMesas = await _context.Mesas.CountAsync();

            int cantidadCocina = await _context.Pedidos
                .Where(p => p.Estado == "LISTO PARA PREPARAR")
                .CountAsync();

            int cantidadMesero = await _context.Pedidos
                .Where(p => p.Estado != "ENTREGADO")
                .CountAsync();

            ViewBag.CantidadMesas = cantidadMesas;

            ViewBag.CantidadRoles = cantidadRoles;

            ViewBag.CantidadFacturas = cantidadFacturas;

            ViewBag.CantidadComidas = cantidadComidas;

            ViewBag.CantidadUsuarios = cantidadUsuarios;

            ViewBag.CantidadOrdenes = cantidadOrdenes;

            ViewBag.CantidadCocina = cantidadCocina;

            ViewBag.CantidadMesero = cantidadMesero;

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
