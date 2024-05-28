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
    }
}
