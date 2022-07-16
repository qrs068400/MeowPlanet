﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


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
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }

            //var count = _context.Members.Count(p => p.Email == member.Email && p.Password == member.Password);

            //return Json(count);
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

        public IActionResult VerifyEmail(Member member)
        {
            var count = _context.Members.Count(p => p.Email == member.Email);
            if (count > 0)
            {
                return Json("已存在");
            }
            return Json(true);

        }
    }
}