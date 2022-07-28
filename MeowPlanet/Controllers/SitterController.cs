// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using MeowPlanet.Models;
using MeowPlanet.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

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
            List<SitterViewModels> sitterAllInfoList = new List<SitterViewModels>();
            var sitterList = _context.Sitters.Where(m => m.IsService == true).ToList();
            foreach (var item in sitterList)
            {
                SitterViewModels model = new SitterViewModels()
                {
                    sitter = item,
                    sitterfeatureList = _context.SitterFeatures.Include(x => x.Feature).Where(m => m.ServiceId == item.ServiceId).Select(x => x.Feature.Name).ToList(),
                    OrderCommentList = _context.Orderlists.Where(m => m.ServiceId == item.ServiceId).ToList(),
                    memberPhoto = _context.Members.FirstOrDefault(m => m.MemberId == item.MemberId).Photo,

                };
                sitterAllInfoList.Add(model);
            }
            return View(sitterAllInfoList);
        }

        [HttpGet]
        public ActionResult GetSitter()
        {
            List<SitterViewModels> sitterAllInfoList = new List<SitterViewModels>();
            var sitterList = _context.Sitters.Where(m => m.IsService == true).ToList();
            foreach (var item in sitterList)
            {
                SitterViewModels model = new SitterViewModels()
                {
                    sitter = item,
                    sitterfeatureList = _context.SitterFeatures.Include(x => x.Feature).Where(m => m.ServiceId == item.ServiceId).Select(x => x.Feature.Name).ToList(),
                    OrderCommentList = _context.Orderlists.Where(m => m.ServiceId == item.ServiceId).ToList(),
                    memberPhoto = _context.Members.FirstOrDefault(m => m.MemberId == item.MemberId).Photo,

                };
                sitterAllInfoList.Add(model);
            }
            return Json(sitterAllInfoList);
        }

        public ActionResult GetItems()
        {
            var itemList = _context.ItemsViewModels.FromSqlRaw("SELECT missing.missing_id AS MissingId,img_01 AS Image,cat.name AS Name,date AS MissingDate,cat_breed.name AS Breed,COUNT(clue.clue_id) AS ClueCount,MAX(witness_time) AS UpdateDate FROM missing INNER JOIN cat ON cat.cat_id = missing.cat_id INNER JOIN cat_breed ON cat.breed_id = cat_breed.breed_id LEFT JOIN clue ON missing.missing_id = clue.missing_id GROUP BY missing.missing_id, img_01, cat.name, date, cat_breed.name").ToList();

            return PartialView("_MissingItemsPartial", itemList);
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
                sitterfeatureList = await _context.SitterFeatures.Include(x => x.Feature).Where(m => m.ServiceId == id).Select(x => x.Feature.Name).ToListAsync(),
                OrderCommentList = await _context.Orderlists.Where(m => m.ServiceId == id).ToListAsync(),

            };
            if (model.sitter == null)
            {
                return NotFound();
            }
            return View(model);
        }



        public ActionResult Order(string[]? date)
        {
            ViewBag.Date = date;
            return View();
        }


    }
}
