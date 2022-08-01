using Microsoft.AspNetCore.Mvc;
using MeowPlanet.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GeoCoordinatePortable;
using MeowPlanet.ViewModels.Missings;

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




            return View();
        }

    }
}
