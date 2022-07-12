using Microsoft.AspNetCore.Mvc;

namespace MeowPlanet.Controllers
{
    public class AdoptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
