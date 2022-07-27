﻿using MeowPlanet.Data;
using MeowPlanet.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MeowPlanet.ViewModels.MemberInfo;


namespace MeowPlanet.Controllers
{
    public class MemberController : Controller
    {
        private readonly endtermContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MemberController(endtermContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            var LoginId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);

            var LoginInfo = _context.Members.FirstOrDefault(p => p.MemberId == LoginId);

            

            return View(LoginInfo);
        }

        public IActionResult CreateCat()
        {
            return View();
        }


        // 回應MyAccountPartial
        [HttpGet]
        public ActionResult GetAccountInfo(int MemberId)
        {
            var MemberInfo = _context.Members.FirstOrDefault(p => p.MemberId == MemberId);
            
            return PartialView("_MyAccountPartial", MemberInfo);
        }



        // 建立貓咪資料
        [HttpPost]
        public async Task<IActionResult> AddCat(Cat cat, IFormFile file1 , IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5)
        {
            Random random = new Random();
            string? uniqueFileName = null; 

            if (file1 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000,9999).ToString() + file1.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file1.CopyTo(fileStream);
                }
                cat.Img01 = "/images/userUpload/" + uniqueFileName;
            }

            if (file2 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file2.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file2.CopyTo(fileStream);
                }
                cat.Img02 = "/images/userUpload/" + uniqueFileName;
            }

            if (file3 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file3.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file3.CopyTo(fileStream);
                }
                cat.Img03 = "/images/userUpload/" + uniqueFileName;
            }

            if (file4 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file4.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file4.CopyTo(fileStream);
                }
                cat.Img04 = "/images/userUpload/" + uniqueFileName;
            }

            if (file5 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file5.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file5.CopyTo(fileStream);
                }
                cat.Img05 = "/images/userUpload/" + uniqueFileName;
            }

            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 取得選擇貓咪資料
        [HttpGet]
        public ActionResult GetCatInfo()
        {
            var LoginId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);

            var CatInfo = _context.Cats.Where(x => x.MemberId == LoginId).Join(_context.CatBreeds,
                c => c.BreedId,
                s => s.BreedId,
                (c, s) => new CatSelectViewModel
                {
                    CatId = c.CatId,
                    Breed = s.Name,
                    Name = c.Name,
                    Sex = c.Sex,
                    Img01 = c.Img01

                }).ToList();                           

            return PartialView("_CatSelectPartial", CatInfo);
        }

        // 取得貓咪詳細資料
        [HttpGet]
        public ActionResult GetCatDetail(int CatId)
        {
            var LoginId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);

            var CatDetail = _context.Cats.Where(x => x.CatId == CatId).Join(_context.CatBreeds,
                c => c.BreedId,
                s => s.BreedId,
                (c, s) => new CatDetailViewModel
                {
                    MemberId = LoginId,
                    CatId = c.CatId,
                    Name = c.Name,
                    Breed = s.Name,
                    Sex = c.Sex,
                    Age = c.Age,
                    Introduce = c.Introduce,
                    Img01 = c.Img01,
                    Img02 = c.Img02,
                    Img03 = c.Img03,
                    Img04 = c.Img04,
                    Img05 = c.Img05,
                    City = c.City,
                    IsAdoptable = c.IsAdoptable,
                    IsMissing = c.IsMissing,
                    IsSitting = c.IsSitting

                }).FirstOrDefault();

            return PartialView("_CatDetailPartial", CatDetail);
        }

        // 修改貓咪資料
        [HttpPost]
        public async Task<IActionResult> UpdateCat(Cat cat, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5)
        {
            Random random = new Random();
            string? uniqueFileName = null;

            if (file1 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file1.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file1.CopyTo(fileStream);
                }
                cat.Img01 = "/images/userUpload/" + uniqueFileName;
            }

            if (file2 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file2.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file2.CopyTo(fileStream);
                }
                cat.Img02 = "/images/userUpload/" + uniqueFileName;
            }

            if (file3 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file3.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file3.CopyTo(fileStream);
                }
                cat.Img03 = "/images/userUpload/" + uniqueFileName;
            }

            if (file4 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file4.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file4.CopyTo(fileStream);
                }
                cat.Img04 = "/images/userUpload/" + uniqueFileName;
            }

            if (file5 != null)  //handle iformfile
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/userUpload");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + file5.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file5.CopyTo(fileStream);
                }
                cat.Img05 = "/images/userUpload/" + uniqueFileName;
            }

            var oldCat = _context.Cats.FirstOrDefault(p => p.CatId == cat.CatId);

            oldCat.Name = cat.Name;
            oldCat.Age = cat.Age;
            oldCat.Sex = cat.Sex;
            oldCat.City = cat.City;
            oldCat.Introduce = cat.Introduce;
            oldCat.IsAdoptable = cat.IsAdoptable;
            oldCat.IsSitting = cat.IsSitting;
            oldCat.IsMissing = cat.IsMissing;
            if(cat.Img01 != null)
            {
                oldCat.Img01 = cat.Img01;
            }
            if(cat.Img02 != null)
            {
                oldCat.Img02 = cat.Img02;
            }
            if (cat.Img03 != null)
            {
                oldCat.Img03 = cat.Img03;
            }
            if (cat.Img04 != null)
            {
                oldCat.Img04 = cat.Img04;
            }
            if (cat.Img05 != null)
            {
                oldCat.Img05 = cat.Img05;
            }
            
            
            
            

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // 修改會員資料
        [HttpPost]
        public async Task<IActionResult> UpdateMember(Member member)
        {
            var oldMember = _context.Members.FirstOrDefault(p => p.MemberId == member.MemberId);

            oldMember.Password = member.Password;
            oldMember.Name = member.Name;
            oldMember.Phone = member.Phone;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
