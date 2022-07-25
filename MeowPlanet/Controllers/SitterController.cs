// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using MeowPlanet.Models;
using MeowPlanet.ViewModels;
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
            return View();
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
                    sitterfeatureList = _context.SitterFeatures.Where(m => m.ServiceId == item.ServiceId).ToList(),
                    OrderCommentList = _context.Orderlists.Where(m => m.ServiceId == item.ServiceId).ToList(),
                    memberPhoto = _context.Members.FirstOrDefault(m => m.MemberId == item.MemberId).Photo,
                };
                sitterAllInfoList.Add(model);
            }
            return Json(sitterAllInfoList);
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

        public ActionResult Order(string[]? date)
        {
            ViewBag.Date = date;
            return View();
        }

    }
}
