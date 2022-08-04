using MeowPlanet.ViewModels;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MeowPlanet.Controllers
{
    public class MessageController : Controller
    {
        private readonly endtermContext _context;
        private readonly int _memberId;

        public MessageController(endtermContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;

            if (contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid) != null)
            {
                _memberId = Convert.ToInt32(contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            }
        }

        [HttpPost]
        public ActionResult sendMessage(string message, int receivedId)
        {
            _context.Messages.Add(new Message
            {
                SendId = _memberId,
                MessageContent = message,
                ReceivedId = receivedId,
                Time = DateTime.Now
            });

            _context.SaveChanges();

            return Content("OK");
        }

        public async Task<IActionResult> SendImage(IFormFile image, int receivedId, [FromServices] IWebHostEnvironment _webHostEnvironment)
        {
            Random random = new Random();
            string uniqueFileName = "";

            if (image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/message");
                uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                await _context.Messages.AddAsync(new Message
                {
                    SendId = _memberId,
                    MessageContent = "/images/message/" + uniqueFileName,
                    ReceivedId = receivedId,
                    Time = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }            

            return Content("/images/message/" + uniqueFileName);
        }

        public async Task<IActionResult> GetHistory(int memberId)
        {
            var result = await _context.Messages.Include(x => x.Send).Include(x => x.Received)
                         .Where(x => (x.SendId == memberId && x.ReceivedId == _memberId) || (x.SendId == _memberId && x.ReceivedId == memberId)).OrderByDescending(x => x.Time).Take(50).Reverse().ToListAsync();
            return PartialView("_HistoryMessagePartial", result);
        }

        public ActionResult SearchMember(int memberId)
        {
            var result = _context.Members.Where(x => x.MemberId == memberId).Select(x => new
            {
                x.Name,
                x.Photo
            }).FirstOrDefault();

            return Json(result);
        }

        public ActionResult MessageRead(int selfId, int memberId)
        {
            _context.Messages.Where(x => x.SendId == memberId && x.ReceivedId == selfId && x.IsRead == false)
                .ToList()
                .ForEach(x => x.IsRead = true);

            _context.SaveChanges();

            return Content("OK");
        }
    }
}
