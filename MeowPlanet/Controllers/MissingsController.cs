using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeowPlanet.ViewModels.Missings;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Data.SqlClient;

namespace MeowPlanet.Models
{
    public class MissingsController : Controller
    {
        private readonly endtermContext _context;
        private int? _memberId;

        public MissingsController(endtermContext context)
        {
            _context = context;

        }



        // GET: Missings
        public async Task<IActionResult> Index()
        {
            List<ItemsViewModel> itemList;

            if (User.FindFirst(ClaimTypes.Sid) != null)
            {
                _memberId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);

                var memberId = new SqlParameter("memberId", _memberId);
                itemList = await _context.ItemsViewModels.FromSqlRaw("SELECT missing.missing_id AS MissingId, sex AS Sex, img_01 AS Image, cat.name AS Name, date AS MissingDate, cat_breed.name AS Breed, COUNT(clue.clue_id) AS ClueCount, MAX(witness_time) AS UpdateDate FROM missing INNER JOIN cat ON cat.cat_id = missing.cat_id INNER JOIN cat_breed ON cat.breed_id = cat_breed.breed_id LEFT JOIN clue ON missing.missing_id = clue.missing_id WHERE is_found = 0 and cat.member_id != @memberId GROUP BY missing.missing_id, sex, img_01, cat.name, date, cat_breed.name", memberId).ToListAsync();
            }
            else
            {
                itemList = await _context.ItemsViewModels.FromSqlRaw("SELECT missing.missing_id AS MissingId, sex AS Sex, img_01 AS Image, cat.name AS Name, date AS MissingDate, cat_breed.name AS Breed, COUNT(clue.clue_id) AS ClueCount, MAX(witness_time) AS UpdateDate FROM missing INNER JOIN cat ON cat.cat_id = missing.cat_id INNER JOIN cat_breed ON cat.breed_id = cat_breed.breed_id LEFT JOIN clue ON missing.missing_id = clue.missing_id WHERE is_found = 0 GROUP BY missing.missing_id, sex, img_01, cat.name, date, cat_breed.name").ToListAsync();
            }
                        

            return View(itemList);
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
        public async Task<IActionResult> GetMissing()
        {
            List<Missing> catList;

            if (_memberId != null)
            {
                catList = await _context.Missings.Include(x => x.Cat).Where(x => x.Cat.MemberId != _memberId).ToListAsync();
            }
            else
            {
                catList = await _context.Missings.ToListAsync();
            }

            return Json(catList);
        }




        public async Task<IActionResult> GetDetail(int missingId)
        {
            var result = await _context.Missings
                .Where(x => x.MissingId == missingId)
                .Include(x => x.Cat)
                .Include(x => x.Cat.Member)
                .Include(x => x.Cat.Breed)
                .Select(x => new DetailViewModel()
                {
                    Name = x.Cat.Name,
                    Sex = x.Cat.Sex,
                    Age = x.Cat.Age,
                    Breed = x.Cat.Breed.Name,
                    Date = x.Date,
                    Img01 = x.Cat.Img01,
                    Img02 = x.Cat.Img02,
                    Img03 = x.Cat.Img03,
                    Description = x.Description,
                    MemberName = x.Cat.Member.Name,
                    Photo = x.Cat.Member.Photo
                })
                .FirstOrDefaultAsync();

            return PartialView("_MissingDetailPartial" ,result);
        }

        public ActionResult GetPublish()
        {            
            return PartialView("_MissingPublishPartial");
        }

        public string PostClue(decimal WitnessLat, decimal WitnessLng, IFormFile Image, DateTime WitnessTime, string Description, int MissingId, [FromServices] IWebHostEnvironment _webHostEnvironment)
        {
            var clue = new Clue
            {
                MissingId = MissingId,
                WitnessLat = WitnessLat,
                WitnessLng = WitnessLng,
                WitnessTime = WitnessTime,
                Description = Description,
                MemberId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value)
            };

            Random random = new Random();
            string? uniqueFileName;

            if (Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/clue");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
                clue.ImagePath = "/images/clue/" + uniqueFileName;
            }

            _context.Clues.Add(clue);
            _context.SaveChanges();

            return "OK";
        }
    }
}
