using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                Time = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Content("OK");
        }
    }
}
