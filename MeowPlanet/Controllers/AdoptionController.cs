using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeowPlanet.Models;
using MeowPlanet.ViewModels;
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
            var catDtoList = new List<CatsDto>();
            catDtoList = GetCatsDto();
            var cats = catDtoList.OrderBy(x => Guid.NewGuid()).ToList();
            if (catDtoList.Count() > 0)
            {
                ViewBag.catid = cats.ToList()[0].CatId;
            }
            return View(cats); //排序及查詢條件

        }

        [HttpPost]
        public JsonResult Next()
        {
            var catDtoList = new List<CatsDto>();
            var CatsDto = GetCatsDto().OrderBy(x => Guid.NewGuid()).ToList(); //排序及查詢條件
            ViewBag.catid = CatsDto[0].CatId;
            var html = "";
            html = "<div class='box1 rotate-vert-center'>";
            html += "<div class='autoplay col-12'>";
            if (CatsDto[0].CatImg1 != null) {
                var CatImg1 = Url.Content(CatsDto[0].CatImg1);
                html += "<div class='slide-1'><img src='" + CatImg1 + "'></div>";
            }
            if (CatsDto[0].CatImg2 != null)
            {
                var CatImg2 = Url.Content(CatsDto[0].CatImg2);
                html += "<div class='slide-2'><img src='" + CatImg2 + "'></div>";
            }
            if (CatsDto[0].CatImg3 != null)
            {
                var CatImg3 = Url.Content(CatsDto[0].CatImg3);
                html += "<div class='slide-3'><img src='" + CatImg3 + "'></div>";
            }
            if (CatsDto[0].CatImg4 != null)
            {
                var CatImg4 = Url.Content(CatsDto[0].CatImg4);
                html += "<div class='slide-4'><img src='" + CatImg4 + "'></div>";
            }
            if (CatsDto[0].CatImg5 != null)
            {
                var CatImg5 = Url.Content(CatsDto[0].CatImg5);
                html += "<div class='slide-5'><img src='" + CatImg5 + "'></div>";
            }
            html += "</div>";
            html += "<div class='context trun front col-12'>";
            html += "<div class='title'>" + CatsDto[0].CatName + "</div>";
            html += "<div class='a1'>";
            html += "<span>" + CatsDto[0].BreedName + " </span>";
            if (CatsDto[0].CatSex == false)
            {
                html += "<i class='fa-solid fa-venus' style='color:#FF8EFF;font-size:1.1rem'></i>";
            } else if (CatsDto[0].CatSex == true) {
                html += "<i class='fa-solid fa-mars' style='color:#99b8d4;font-size:1.1rem'></i>";
            }
            html += "<p class='age'>" + CatsDto[0].CatAge + " 歲</p>";
            html += "<p>" + CatsDto[0].CatCity + "</p>";
            html += "<p class='introduce'>" + CatsDto[0].CatIntroduce + "</p>";
            html += "</div>";
            html += "</div>";
            html += "<div class='like' style='text-align:center;'>";
            html += "<button id='like'onclick='checklike2()' type='button' class='btn btn-outline-danger buzz-pri pulsate-bck'>";
            html += "<svg xmlns='http://www.w3.org/2000/svg' style='padding-bottom:3px;' width='18' height='18' fill='currentColor' class='bi bi-heart-fill' viewBox='0 0 16 16'>";
            html += "<path fill-rule='evenodd' d='M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314z' />";
            html += "</svg> 認養";
            html += "</button>";
            html += "<button id='pass' type='button' class='btn btn-outline-secondary buzz' style='margin-left: 5px;'>PASS</button>";
            html += "</div>";
            html += "</div>";

            ViewBag.catid = CatsDto[0].CatId;

            return Json(new { item = html, catId = CatsDto[0].CatId });
        }

        public List<CatsDto> GetCatsDto()
        {
            var memberid = 1;
            var catBreeds = _context.CatBreeds.ToList();
            var adopt = _context.Adopts.Where(x => x.MemberId == memberid).ToList();
            var catDtoList = new List<CatsDto>();
            var catList = _context.Cats.Where(x => x.MemberId != memberid && x.IsAdoptable == true).ToList();
            for (int i = 0; i < catList.Count(); i++)
            {
                if (adopt.Where(x => x.CatId == catList[i].CatId).Count() <= 0)
                {
                    var catName = catBreeds.FirstOrDefault(x => x.BreedId == catList[i].BreedId).Name;
                    var catDto = new CatsDto
                    {
                        CatId = catList[i].CatId,
                        MemberId = catList[i].MemberId,
                        CatName = catList[i].Name,
                        CatSex = catList[i].Sex,
                        CatAge = catList[i].Age,
                        CatIntroduce = catList[i].Introduce,
                        CatImg1 = catList[i].Img01,
                        CatImg2 = catList[i].Img02,
                        CatImg3 = catList[i].Img03,
                        CatImg4 = catList[i].Img04,
                        CatImg5 = catList[i].Img05,
                        CatIsAdoptable = catList[i].IsAdoptable,
                        BreedName = catName,
                        CatCity = catList[i].City,
                    };
                    catDtoList.Add(catDto);

                }
            }
            return catDtoList;

        }

        [HttpPost]
        public JsonResult Like(Adopt adopt ,int catid)
        {
            var v = @ViewBag.catid;
            //if (Session["emp"] == null)
            //{
            //    return RedirectToAction("Login", "Members");
            //}
            var memberid = 1;
            adopt.MemberId = memberid;
            adopt.CatId = catid;
            adopt.DateStart = DateTime.Today;
            adopt.DateOver = null;
            adopt.Status = false;
            _context.Adopts.Add(adopt);
            _context.SaveChanges();
            return null;
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
