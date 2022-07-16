using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MeowPlanet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemberRepostry _mem;

        public HomeController(ILogger<HomeController> logger,IMemberRepostry mem)
        {
            _logger = logger;
            _mem = mem;
        }

        public IActionResult Index()
        {
            _mem.selectMember(2);
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