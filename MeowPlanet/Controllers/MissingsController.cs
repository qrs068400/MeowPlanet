using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace MeowPlanet.Models
{
    public class MissingsController : Controller
    {
        private readonly endtermContext _context;

        public MissingsController(endtermContext context)
        {
            _context = context;
        }

        // GET: Missings
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddMissing(Missing missingCat)
        {
            _context.Missings.Add(missingCat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult GetMissing()
        {
            List<Missing> catList = new List<Missing>();
            foreach (var item in _context.Missings)
            {
                Missing missingCat = new Missing
                {
                    CatId = item.CatId,
                    Date = item.Date,
                    Lat = item.Lat,
                    Lng = item.Lng
                };

                catList.Add(missingCat);
            }

            var data = catList;
            return Json(data);
        }

    }
}
