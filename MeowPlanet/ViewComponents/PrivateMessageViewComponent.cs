using Microsoft.AspNetCore.Mvc;
using MeowPlanet.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GeoCoordinatePortable;
using MeowPlanet.ViewModels;

namespace MeowPlanet.ViewComponents
{
    public class PrivateMessageViewComponent : ViewComponent
    {

        private readonly endtermContext _context;

        public PrivateMessageViewComponent(endtermContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int memberId)
        {

            var contactMembers = _context.Messages
                .Where(x => x.ReceivedId == memberId || x.SendId == memberId)
                .OrderBy(x => x.Time)
                .Select(x => new { x.Send, x.Received })
                .Distinct()
                .Select(x => new ContactMembers
                {
                    Id = x.Send.MemberId == memberId ? x.Received.MemberId : x.Send.MemberId,
                    Name = x.Send.MemberId == memberId ? x.Received.Name : x.Send.Name,
                    unreadCount = _context.Messages.Count(m => m.IsRead == false && m.SendId == (x.Send.MemberId == memberId ? x.Received.MemberId : x.Send.MemberId))
                })
                .ToList();
                

            var result = _context.Members.Where(m => m.MemberId == memberId).Select(x => new MessageBoxModel
            {
                userName = x.Name,
                userPhoto = x.Photo,
                ContactMembers = contactMembers

            }).FirstOrDefault();



            return View(result);
        }

    }
}
