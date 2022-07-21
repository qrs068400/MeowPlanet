using MeowPlanet.Data;
using MeowPlanet.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


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
            return View();
        }

        public IActionResult CreateCat()
        {
            return View();
        }

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
    }
}
