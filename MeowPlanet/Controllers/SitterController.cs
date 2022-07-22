using Microsoft.AspNetCore.Mvc;
using MeowPlanet.ViewModels;
using MeowPlanet.Models;
using Microsoft.EntityFrameworkCore;

namespace MeowPlanet.Controllers
{
    public class SitterController : Controller
    {
        private readonly endtermContext _context;

        public SitterController(endtermContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var c = _context.Sitters.Include(p => p.IsService == true);
            return View(c);
        }
        

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SitterViewModels model = new SitterViewModels()
            {
                sitter = await _context.Sitters.FirstOrDefaultAsync(m => m.ServiceId == id),
                sitterfeatureList = await _context.SitterFeatures.Where(m => m.ServiceId == id).ToListAsync(),
                OrderCommentList = await _context.Orderlists.Where(m => m.ServiceId == id).ToListAsync(),

            };

            
            if (model.sitter == null)
            {
                return NotFound();
            }
           
            
            return View(model);
        }

        public async Task<IActionResult> Order(string[]? date)
        {
            ViewBag.Date = date;
            return View();
        }

    }
}
