using Dal.Dragones;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Bar_Restaurante_Los_Dragones.Models;

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
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == correo && u.Clave == clave);

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim("Correo", usuario.Correo),
                };
                foreach (string rol in usuario.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Error"] = "El correo electrónico o la contraseña son incorrectos.";
                return View();
            }
        }

        public IActionResult Register()
        {
            // Aquí puedes inicializar el usuario con el rol por defecto
            var usuario = new Usuario
            {
                Roles = new string[] { "Cliente" } // Rol por defecto
            };
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario usuario)
        {
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
