using Dal.Dragones;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Bar_Restaurante_Los_Dragones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProyectoContext _context;

        public LoginController(ProyectoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string correo, string clave)
        {
            var usuario = _context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.Correo == correo && u.Clave == clave);

            if (usuario != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim("Correo", usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("home", "Home");
            }
            else
            {
                ViewData["Error"] = "El correo electrónico o la contraseña son incorrectos.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            // Aquí puedes inicializar el usuario con el rol por defecto
            var defaultRol = _context.Roles.FirstOrDefault(r => r.Nombre == "Empleado");
            if (defaultRol == null)
            {
                // Si el rol no existe, podrías crear uno por defecto
                defaultRol = new Rol { Nombre = "Empleado" };
                _context.Roles.Add(defaultRol);
                _context.SaveChanges();
            }

            ViewBag.DefaultRolID = defaultRol.ID;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            ModelState.Remove("Rol");
            if (ModelState.IsValid)
            {
                // Verificar si el correo electrónico ya está registrado
                if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
                {
                    ModelState.AddModelError("Correo", "El correo electrónico ya está registrado.");
                    return View(usuario);
                }

                // Agregar el usuario a la base de datos
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // Redirigir al usuario a la página de inicio de sesión
                return RedirectToAction("Index");
            }
            return View(usuario);
        }


        public async Task<IActionResult> Salir()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");

        }
    }
}
