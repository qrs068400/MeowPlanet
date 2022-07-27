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
using System.Net.Mail;

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

        public IActionResult Password()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        // 重置密碼
        [HttpGet]
        public async Task<IActionResult> DoPwdReset(string email, string password)
        {

            var UserInfo = _context.Members.FirstOrDefault(x => x.Email == email);

            UserInfo.Password = password;

            await _context.SaveChangesAsync();
            return Content("修改完成");
        }

        // 登入判定Email及密碼是否正確
        [HttpGet]
        public ActionResult LoginCheck(string email, string password)
        {
            var count = _context.Members.Count(x => x.Email == email && x.Password == password);

            if (count > 0)
            {
                return Content("");
            }
            else
            {
                return Content("Email或密碼錯誤");
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
                return NoContent();
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        // 註冊判定Email是否可使用
        [HttpGet]
        public ActionResult EmailCheck(string email)
        {

            var count = _context.Members.Count(x => x.Email == email);

            if (count > 0)
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

        // 寄驗證信
        public ActionResult SendEmailMsg(string Email)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(Email);

            msg.From = new MailAddress("meowplanet04@gmail.com", "喵屋星球", System.Text.Encoding.UTF8);

            msg.Subject = "重新設定您在 MeowPlanet 的密碼";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "這是 MeowPlanet 喵屋星球 的密碼重置信，若你不曾要求重設密碼，請忽略這封信<br />" +
                "<a href='" + "https://localhost:44394/Login/ResetPassword" + "?Email=" + Email + "'>請點擊此連結重置密碼</a>" + "<br /><br />MeowPlanet 喵屋星球";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("meowplanet04@gmail.com", "cbqjonjcosbrqnnv");
            client.Host = "smtp.gmail.com";
            client.Port = 25;
            client.EnableSsl = true;
            client.Send(msg);
            client.Dispose();
            msg.Dispose();

            return NoContent();
        }

        [HttpGet]
        public ActionResult ForgetPassword(string ForgetEmail)
        {
            var count = _context.Members.Count(x => x.Email == ForgetEmail);

            if (count > 0)
            {
                SendEmailMsg(ForgetEmail);

                return Content("驗證信已寄出");
            }
            else
            {
                return Content("此信箱不存在");
            }

        }
    }
}
