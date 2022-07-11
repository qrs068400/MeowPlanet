using MeowPlanet.Data;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeowPlanet.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
