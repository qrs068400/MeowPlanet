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
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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

        // 登入判定Email是否存在
        [HttpGet]
        public ActionResult EmailExist(string email)
        {
            var count = _context.Members.Count(x => x.Email == email);

            if(count > 0)
            {
                return Content("");
            }
            else
            {
                return Content("此信箱不存在");
            }
        }

        // 登入
        [HttpPost]
        public IActionResult Login(Member member)
        {
            var count = _context.Members.Count(p => p.Email == member.Email && p.Password == member.Password);

            if (count > 0)
            {
                var LoginInfo = _context.Members.FirstOrDefault(p => p.Email == member.Email);

                var LoginId = LoginInfo.MemberId;

                var LoginName = LoginInfo.Name;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, LoginId.ToString()),
                    new Claim(ClaimTypes.Name, LoginName),

                };

                var claimsIdentity = new ClaimsIdentity(
                           claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                        new ClaimsPrincipal(claimsIdentity));

                

                return RedirectToAction("Index", "Member");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");
        }

        // 註冊判定Email是否可使用
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

        // 註冊
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
