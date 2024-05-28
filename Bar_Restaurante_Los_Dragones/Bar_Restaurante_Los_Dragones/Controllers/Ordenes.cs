using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class Ordenes : Controller
    {
        public ActionResult Orden()
        {
            return View();
        }
        public ActionResult Facturas()
        {
            return View();
        }
        public ActionResult Historial()
        {
            return View();
        }
        public ActionResult NuevaOrden()
        {
            return View();
        }
    }
}
