using MeowPlanet.Data;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeowPlanet.Controllers
{
    public class MemberController : Controller
    {
        private readonly endtermContext _context;

        public MemberController(endtermContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCat(Cat cat)
        {
            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
