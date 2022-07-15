using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MeowPlanet.Controllers
{

    public class AdoptionController : Controller
    {
        private readonly endtermContext _context;

        public AdoptionController(endtermContext context)
        {
            _context = context;
        }

        // GET: AdoptionController
        public ActionResult Index()
        {



            var Lambdajoin = _context.CatBreeds.Join(_context.Cats, //第一個參數為 要加入的資料來源
            c => c.BreedId,//主表要join的值
            s => s.BreedId,//次表要join的值
            (c, s) => new  // (c,s)代表將資料集合起來
            {
                CatId = s.CatId,
                MemberId = s.MemberId,
                CatName = s.Name,
                CatSex = s.Sex,
                CatAge = s.Age,
                CatIntroduce = s.Introduce,
                CatImg1 = s.Img01,
                CatImg2 = s.Img02,
                CatImg3 = s.Img03,
                CatImg4 = s.Img04,
                CatImg5 = s.Img05,
                CatIsAdoptable = s.IsAdoptable,
                BreedName = c.Name,

            }).Where(cs => cs.CatIsAdoptable == true).OrderBy(x => Guid.NewGuid()).Take(1);//排序及查詢條件

            //var adoptcat = _context.Cats.Where(x => x.IsAdoptable == true).ToList();


            return View(Lambdajoin);
        }

        // GET: AdoptionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdoptionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdoptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdoptionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdoptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdoptionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdoptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
