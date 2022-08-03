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
            if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var count = _context.Members.Count(x => x.Email == email);
                
                if(count > 0)
                {
                    var count2 = _context.Members.Count(x => x.Email == email && x.Password == password);

                    if (count2 > 0)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return Content("密碼錯誤");
                    }
                }
                else
                {
                    return Content("此Email不存在");
                }
                

            }
            return NoContent();

        }

        // 登入
        [HttpPost]
        public ActionResult Login(Member member,string rememberme)
        {
            var count = _context.Members.Count(p => p.Email == member.Email && p.Password == member.Password);

            if (count > 0)
            {
                var LoginInfo = _context.Members.FirstOrDefault(p => p.Email == member.Email); //整筆資料取出

                var LoginId = LoginInfo.MemberId;

                var LoginName = LoginInfo.Name;
                
                //cookie驗證
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, LoginId.ToString()),
                    new Claim(ClaimTypes.Name, LoginName),

                };

                var claimsIdentity = new ClaimsIdentity(
                           claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //記住我
                var properties = new AuthenticationProperties
                {
                    IsPersistent = Convert.ToBoolean(rememberme),
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                };

                //登入驗證存進cookie
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                        new ClaimsPrincipal(claimsIdentity), properties);


<<<<<<< HEAD
=======
                string controller = "";
                string action = "";
                string id = "";
>>>>>>> rt1

                if (TempData.ContainsKey("Controller"))
                    controller = TempData["Controller"].ToString();
                if (TempData.ContainsKey("Action"))
                    action = TempData["Action"].ToString();
                if (TempData.ContainsKey("id"))
                {
                    id = TempData["id"].ToString();
                    return RedirectToAction(action, controller, new { id = id });
                }
                else
                {
                    return RedirectToAction(action, controller);
                }
                //return RedirectToAction("Index", "Member");
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
            if (!string.IsNullOrEmpty(email))
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

            return NoContent();
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
