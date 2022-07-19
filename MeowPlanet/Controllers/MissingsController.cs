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
            _context.Cats.Where(x => x.CatId == missingCat.CatId).First().IsMissing = true;

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
            var itemList = _context.ItemsViewModels.FromSqlRaw("SELECT missing.missing_id AS MissingId, sex AS Sex, img_01 AS Image, cat.name AS Name, date AS MissingDate, cat_breed.name AS Breed, COUNT(clue.clue_id) AS ClueCount, MAX(witness_time) AS UpdateDate FROM missing INNER JOIN cat ON cat.cat_id = missing.cat_id INNER JOIN cat_breed ON cat.breed_id = cat_breed.breed_id LEFT JOIN clue ON missing.missing_id = clue.missing_id WHERE is_found = 0 GROUP BY missing.missing_id, sex, img_01, cat.name, date, cat_breed.name").ToList();

            return PartialView("_MissingItemsPartial", itemList);
        }


        public ActionResult GetDetail(int missingId)
        {
            var result = _context.Missings
                .Where(x => x.MissingId == missingId)
                .Include(x => x.Cat)
                .Include(x => x.Cat.Member)
                .Select(x => new DetailViewModel()
                {
                    Name = x.Cat.Name,
                    Sex = x.Cat.Sex,
                    Age = x.Cat.Age,
                    Date = x.Date,
                    Img01 = x.Cat.Img01,
                    Img02 = x.Cat.Img02,
                    Img03 = x.Cat.Img03,
                    Description = x.Description,
                    MemberName = x.Cat.Member.Name,
                    Photo = x.Cat.Member.Photo
                })
                .FirstOrDefault();
            return PartialView("_MissingDetailPartial" ,result);
        }

        public ActionResult GetPublish()
        {
            return PartialView("_MissingPublishPartial");
        }
    }
}
