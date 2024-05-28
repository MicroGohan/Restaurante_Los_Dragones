using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar_Restaurante_Los_Dragones.Controllers
{
    public class Menu : Controller
    {
        public ActionResult AgregarComida()
        {
            return View();
        }
    }
}
