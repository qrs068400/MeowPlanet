using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MeowPlanet.Controllers
{

    public class AdoptionController : Controller
    {
        private readonly endtermContext _context;

        public AdoptionController(endtermContext context)
        {
            _context = context;
        }

        // GET: AdoptionController
        public ActionResult Index()
        {
            var adoptcat=_context.Cats.Where(x => x.IsAdoptable==true).ToList();
           

            return View(adoptcat);
        }

        // GET: AdoptionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdoptionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdoptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdoptionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdoptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdoptionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdoptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
