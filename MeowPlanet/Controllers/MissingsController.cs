using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeowPlanet.ViewModels.Missings;


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
                    MissingId = item.MissingId,
                    CatId = item.CatId,
                    Date = item.Date,
                    Lat = item.Lat,
                    Lng = item.Lng
                };

                catList.Add(missingCat);
            }

            return Json(catList);
        }

        public ActionResult GetItems()
        {
            var itemList = _context.ItemsViewModels.FromSqlRaw("SELECT missing.missing_id AS MissingId, sex AS Sex, img_01 AS Image, cat.name AS Name, date AS MissingDate, cat_breed.name AS Breed, COUNT(clue.clue_id) AS ClueCount, MAX(witness_time) AS UpdateDate FROM missing INNER JOIN cat ON cat.cat_id = missing.cat_id INNER JOIN cat_breed ON cat.breed_id = cat_breed.breed_id LEFT JOIN clue ON missing.missing_id = clue.missing_id GROUP BY missing.missing_id, sex, img_01, cat.name, date, cat_breed.name").ToList();

            return PartialView("_MissingItemsPartial", itemList);
        }

    }
}
