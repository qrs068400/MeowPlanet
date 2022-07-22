using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace MeowPlanet.Controllers
{
    public class LoginController : Controller
    {
        private readonly endtermContext _context;

        public LoginController(endtermContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Member member)
        {
            var count = _context.Members.Count(p => p.Email == member.Email && p.Password == member.Password);

            if (count > 0)
            {
                HttpContext.Session.SetString("LoginMemberEmail",member.Email);

                return RedirectToAction("Index", "Member");
            }
            else
            {
                return RedirectToAction("Index");
            }

            //var count = _context.Members.Count(p => p.Email == member.Email && p.Password == member.Password);

            //return Json(count);
        }

        [HttpGet]
        public ActionResult EmailCheck(string email)
        {

                var count = _context.Members.Count(x => x.Email == email);

                if(count > 0)
                {
                    return Content("此信箱已註冊");
                }
                else
                {
                    return Content("此信箱可使用");
                }
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Members.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Register");
        }
    }
}
