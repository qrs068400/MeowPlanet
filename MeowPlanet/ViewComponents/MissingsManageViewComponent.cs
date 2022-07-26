using Microsoft.AspNetCore.Mvc;
using MeowPlanet.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MeowPlanet.ViewComponents
{
    public class MissingsManageViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke(int memberId, int status, [FromServices] endtermContext _context)
        {
            var result = _context.Clues.Include(c => c.Missing)
                .Include(c => c.Missing.Cat)
                .Where(c => c.Missing.Cat.MemberId == memberId && c.Missing.Cat.IsMissing == true)
                .Where(c => c.Missing.CatId == c.Missing.Cat.CatId)
                .Where(c => c.Missing.MissingId == c.MissingId && c.Status == status).ToList();


            return View(result);
        }

    }
}
