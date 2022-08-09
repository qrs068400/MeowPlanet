// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using MeowPlanet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using MeowPlanet.ViewModels.Sitters;

namespace MeowPlanet.Controllers
{
    public class SitterController : Controller
    {
        private readonly endtermContext _context;
        private readonly int? _memberId;

        public SitterController(endtermContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;

            if (contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid) != null)
            {
                _memberId = Convert.ToInt32(contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            }
        }
        public async Task<IActionResult> Index()
        {
            //三個方法用中斷點看持續時間幾乎都差不多，大概2.5s~3.0s之間跳動
            //但單純看sql語法的秒數又差很多
            //方法一、二的sql 語法速度大概是70ms~90ms
            //方法三的sql 語法速度加總大概 280ms 以上
            //想要用forloop來測試 10圈的速度
            //發現第一圈花了2.X秒，其餘圈數都花<100ms左右而已

            //方法一
            var result = await _context.Sitters
                .AsNoTracking()
                //.AsNoTrackingWithIdentityResolution()
                //.Include(c => c.SitterFeatures)
                .Where(m => m.IsService == true)
                .Select(m => new SitterViewModels
                {
                    sitter = m,
                    memberPhoto = m.Member.Photo,
                    sitterfeatureList = m.SitterFeatures.Select(m => m.Feature.Name).ToList(),
                    OrderCommentList = m.Orderlists.ToList(),
                }).ToListAsync();

            //方法二
            //var result = new List<Sitter>();
            //result = await _context.Sitters
            //.Include(c => c.Member)
            //.Include(c => c.SitterFeatures)
            //.ThenInclude(c => c.Feature)
            //.Where(m => m.IsService == true).ToListAsync();

            //List<SitterViewModels> result2 = new List<SitterViewModels>();
            //foreach (var item in result)
            //{
            //    SitterViewModels x = new SitterViewModels()
            //    {
            //        sitter = item,
            //        memberPhoto = item.Member.Photo,
            //        sitterfeatureList = item.SitterFeatures.Select(c => c.Feature.Name).ToList(),
            //        OrderCommentList = item.Orderlists.ToList(),
            //    };
            //    result2.Add(x);
            //};

            //方法三
            //List<SitterViewModels> result = new List<SitterViewModels>();
            //var sitterList = _context.Sitters.Where(m => m.IsService == true).ToList();
            //foreach (var item in sitterList)
            //{
            //    SitterViewModels model = new SitterViewModels()
            //    {
            //        sitter = item,
            //        sitterfeatureList = _context.SitterFeatures.Include(m => m.Feature).Where(m => m.ServiceId == item.ServiceId).Select(x => x.Feature.Name).ToList(),
            //        OrderCommentList = _context.Orderlists.Where(m => m.ServiceId == item.ServiceId).ToList(),
            //        memberPhoto = _context.Members.FirstOrDefault(m => m.MemberId == item.MemberId).Photo,

            //    };
            //    result.Add(model);
            //}

            TempData["controller"] = "Sitter";
            TempData["action"] = "Index";
            return View(result);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            SitterViewModels model = new SitterViewModels()
            {
                sitter = await _context.Sitters.FirstOrDefaultAsync(m => m.ServiceId == id),
                sitterfeatureList = await _context.SitterFeatures.Include(x => x.Feature).Where(m => m.ServiceId == id).Select(x => x.Feature.Name).ToListAsync(),
                OrderCommentList = await _context.Orderlists.Where(m => m.ServiceId == id).ToListAsync(),

            };
            var catList = await _context.Cats
                .Where(m => m.MemberId == _memberId)
                .Select(m => new Cat
                {
                    CatId = m.CatId,
                    BreedId = m.BreedId,
                    IsAdoptable = m.IsAdoptable,
                    IsMissing = m.IsMissing,
                    IsSitting = m.IsSitting,
                    Name = m.Name,
                    Sex = m.Sex,
                    Introduce = m.Introduce,
                    Img01 = m.Img01,
                }).ToListAsync();

            ViewBag.catList = catList;
            TempData["controller"] = "Sitter";
            TempData["action"] = "Detail";
            TempData["ids"] = $"{id}";
            return View(model);
        }
        //方法一
        // 使用 變數 來接收submit資料
        [HttpPost]
        public ActionResult ComfirmPay(string startDate, string endDate, int night, int catId, int serviceId)
        {
            var user = _context.Members
                .Include(c => c.Cats.Where(c => c.CatId == catId))
                .ThenInclude(c => c.Breed)
                .Where(c => c.MemberId == _memberId)
                .FirstOrDefault();

            var sitter = _context.Sitters
                .Include(c => c.SitterFeatures)
                .Include(c => c.Member)
                .Where(c => c.ServiceId == serviceId).FirstOrDefault();

            SitterComfirmPayViewModels result = new SitterComfirmPayViewModels()
            {
                SitterName = sitter.Member.Name,
                SitterPhoto = sitter.Member.Photo,
                UserName = user.Name,
                UserPhone = user.Phone,
                UserPhoto = user.Photo,
                CatId = catId,
                Sex = user.Cats.FirstOrDefault().Sex,
                Introduce = user.Cats.FirstOrDefault().Introduce,
                BreedName = user.Cats.FirstOrDefault().Breed.Name,
                CatName = user.Cats.Where(c => c.CatId == catId).FirstOrDefault().Name,
                CatImg01 = user.Cats.Where(c => c.CatId == catId).FirstOrDefault().Img01,
                ServiceId = serviceId,
                ServiceName = sitter.Name,
                DateStart = DateTime.ParseExact(startDate, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture).Date,
                DateOver = DateTime.ParseExact(endDate, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture).Date,
                Pay = sitter.Pay,
                Night = night,
                Total = night * sitter.Pay,
            };
            return View(result);
        }
        //方法二
        // 使用 IFormCollection 來接收submit資料
        //[HttpPost]
        //public ActionResult ComfirmPay(IFormCollection form)
        //{
        //    string startDate = form["startDate"].ToString();
        //    string endDate = form["endDate"].ToString();
        //    //string endDate = form["__RequestVerificationToken"].ToString();

        //    ViewBag.start = startDate;
        //    ViewBag.end = endDate;
        //    return View();
        //}
        
        //送出訂單
        [HttpPost]
        public async Task<IActionResult> AddOrder(SitterComfirmPayViewModels sitterComfirmPay)
        {
            Orderlist orderlist = new Orderlist()
            {
                MemberId = (int)_memberId,
                ServiceId = sitterComfirmPay.ServiceId,
                CatId = sitterComfirmPay.CatId,
                DateStart = sitterComfirmPay.DateStart,
                DateOver = sitterComfirmPay.DateOver,
                DateOrder = DateTime.Now,
                Total = sitterComfirmPay.Total,
            };
            _context.Orderlists.Add(orderlist);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SitterViewMode()
        {
            var sitter = _context.Sitters
            //要用theninclude 才看的到 feature
             //.Include(x => x.SitterFeatures)
             //.ThenInclude(x => x.Feature)
             //直接 include 看不到 feature
             //.Include(x => x.SitterFeatures.Feature)
             .Where(x => x.MemberId == _memberId)
             .Select(x => new SitterViewModels
             {
               sitter = x,
               //實際測試 我根本不用上面的include就可以取到我要資料了，這串語法自帶LEFT JOIN 跟 INNER JOIN
               sitterfeatureList = x.SitterFeatures.Select(x => x.Feature.Name).ToList(),
             }).FirstOrDefault();
            return PartialView("_SitterViewModePartial", sitter);
        }

        [HttpPost]
        public ActionResult SitterEditMode([FromBody]SitterViewModels j)
        {
            return PartialView("_SitterEditModePartial", j);
        }
    }
}
