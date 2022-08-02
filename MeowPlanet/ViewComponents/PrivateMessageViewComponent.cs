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
            var result = _context.Members.Where(m => m.MemberId == memberId).Select(x => new MessageBoxModel
            {
                userName = x.Name,
                userPhoto = x.Photo,
            }).FirstOrDefault();



            return View(result);
        }

    }
}
