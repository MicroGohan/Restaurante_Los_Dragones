using Bar_Restaurante_Los_Dragones.Models.ViewModels;
using LosDragones.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class UserController : Controller
    {

        private readonly LosDragonesDBContext _losDragonesContext;
        private readonly AuthContext _authContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public UserController(LosDragonesDBContext losDragonesContext, AuthContext authContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<ApplicationUser> userStore)
        {
            _losDragonesContext = losDragonesContext;
            _authContext = authContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateUser(UserControllerViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();

                user.Email = userModel.Email;
                user.Nombre = userModel.Nombre;
                user.PrimerApellido = userModel.PrimerApellido;
                user.SegundoApellido = userModel.SegundoApellido;
                var result = await _userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    string normalizedNameRole = _roleManager.Roles.FirstOrDefault(r => r.Id == userModel.IdRol).NormalizedName;
                    var roleResult = await _userManager.AddToRoleAsync(user, normalizedNameRole);
                    var userId = await _userManager.GetUserIdAsync(user);

                    return RedirectToAction("CrearEmpleado", "Administracion");
                }
            }
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View(userModel);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult CreateUser()
        {
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View();
        }

        public async Task<IActionResult> DetailsUser(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.Roles = roles;

            return View(user);
        }
    }
}
