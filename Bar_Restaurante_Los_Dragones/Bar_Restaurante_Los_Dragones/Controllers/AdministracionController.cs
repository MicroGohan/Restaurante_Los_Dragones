using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class AdministracionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrador,Empleado")]
        public ActionResult Restaurante()
        {
            return View();
        }
        public ActionResult Proveedor()
        {
            return View();
        }
        public ActionResult ProveedorListado()
        {
            return View();
        }
        public ActionResult AgregarComida()
        {
            return View();
        }
        public ActionResult CrearAdministrador()
        {
            return View();
        }
        public ActionResult AdministradorListado()
        {
            return View();
        }
        public ActionResult CrearEmpleado()
        {
            return View();
        }
        public ActionResult EmpleadoListado()
        {
            return View();
        }
        public ActionResult Reporte()
        {
            return View();
        }
    }
}
