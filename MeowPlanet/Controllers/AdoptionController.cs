using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MeowPlanet.Models;
using MeowPlanet.ViewModels.Adopts;
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

        public ActionResult Index()
        {
            var catDtoList = new List<CatsDto>();
            catDtoList = GetCatsDto(null, 0, null); //初始值: city=null, breedId=0, Sex=null
            var cats = catDtoList.OrderBy(x => Guid.NewGuid()).ToList();
            if (catDtoList.Count() > 0)
            {
                ViewBag.catid = cats.ToList()[0].CatId;
            }
            return View(cats); //排序及查詢條件

        }

        public ActionResult Next(string? city_Utf8, int breedId, bool? catSex)
        {
            var city = HttpUtility.HtmlDecode(city_Utf8);
            var catDtoList = new List<CatsDto>();
            if ((!string.IsNullOrEmpty(city) && city != "全部") || breedId != 0 || catSex != null)
            {
                //查詢條件
                catDtoList = GetCatsDto(city, breedId, catSex).OrderBy(x => Guid.NewGuid()).ToList();
                //暫存查詢條件
                ViewBag.city = (!string.IsNullOrEmpty(city) && city != "全部") ? catDtoList[0].CatCity : "全部";
                ViewBag.breedId = (breedId != 0) ? catDtoList[0].BreedId : 0;
                ViewBag.catSex = (catSex != null) ? catDtoList[0].CatSex : null;

            }
            else
            {
                catDtoList = GetCatsDto(null, 0, null).OrderBy(x => Guid.NewGuid()).ToList(); //初始值: city=null, breedId=0, Sex=null
            }
            ViewBag.catid = catDtoList[0].CatId;



            return PartialView("_CardPartial", catDtoList);
        }

        [HttpPost]
        public JsonResult Like(Adopt adopt, int catid)
        {
            //主人ID
            var owner = _context.Cats
            .Where(x => x.CatId == adopt.CatId).Select(x => x.MemberId).FirstOrDefault();

            var memberid = 1;
            adopt.MemberId = memberid;
            adopt.CatId = catid;
            adopt.DateStart = DateTime.Today;
            adopt.DateOver = null;
            adopt.Status = 0;
            adopt.owner = owner;
            _context.Adopts.Add(adopt);
            _context.SaveChanges();
            return null;
        }


        public ActionResult Search(string? city_Utf8, int breedId, bool? catSex)
        {
            var city = HttpUtility.UrlDecode(city_Utf8); //解碼unicode
            var catDtoList = new List<CatsDto>();
            var CatsDto = GetCatsDto(city, breedId, catSex).OrderBy(x => Guid.NewGuid()).ToList(); //排序及查詢條件

            //暫存查詢條件
            ViewBag.catid = CatsDto[0].CatId;
            ViewBag.city = (!string.IsNullOrEmpty(city) && city != "全部") ? CatsDto[0].CatCity : "全部";
            ViewBag.breedId = (breedId != 0) ? CatsDto[0].BreedId : 0;
            ViewBag.catSex = (catSex != null) ? CatsDto[0].CatSex : null;

            return PartialView("_CardPartial", CatsDto);
        }

        public List<CatsDto> GetCatsDto(string? city, int breedId, bool? catSex)
        {
            var memberid = 1;
            var catBreeds = _context.CatBreeds.ToList();
            //頁面上呈現品種
            ViewBag.CatBreeds = catBreeds;
            var adopt = _context.Adopts.Where(x => x.MemberId == memberid).ToList();
            var catDtoList = new List<CatsDto>();
            var catList = _context.Cats.Where(x => x.MemberId != memberid && x.IsAdoptable == true).ToList();

            #region 搜尋條件判斷
            if (breedId != 0)
            {
                //品種 
                catList = catList.Where(x => x.BreedId == breedId).ToList();
            }
            if (catSex != null)
            {
                //性別
                catList = catList.Where(x => x.Sex == catSex).ToList();
            }
            if (!string.IsNullOrEmpty(city) && city != "全部")
            {
                //地點
                catList = catList.Where(x => x.City == city).ToList();
            }
            #endregion

            for (int i = 0; i < catList.Count(); i++)
            {
                if (adopt.Where(x => x.CatId == catList[i].CatId).Count() <= 0)
                {
                    var catName = catBreeds.FirstOrDefault(x => x.BreedId == catList[i].BreedId).Name;
                    var catDto = new CatsDto
                    {
                        CatId = catList[i].CatId,
                        BreedId = catList[i].BreedId,
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

        public ActionResult LikeTolist()
        {
            //var adoptlike = _context.Adopts.Where(x => x.MemberId == 1).OrderBy(x => x.DateStart).ToList();
            //var adoptlike = _context.Adopts.Include(x => x.Cat).Include(x=>x.Member).Include(x=>x.Cat.BreedId)

            var likeadopts = _context.Adopts
             .Where(x => x.MemberId == 1)
             .Include(x => x.Cat)
             .Include(x => x.Cat.Breed)
             .Select(x => new ViewModels.LikeAdopts()
             {
                 CatName = x.Cat.Name,
                 CatSex = x.Cat.Sex,
                 CatAge = x.Cat.Age,
                 BreedName = x.Cat.Breed.Name,
                 CatImg1 = x.Cat.Img01,
                 DateStart = x.DateStart,
                 DateOver = x.DateOver,
                 Status = x.Status,
                 Owner = x.owner,
             })
             .ToList();


            return View(likeadopts);
        }
    }
}
