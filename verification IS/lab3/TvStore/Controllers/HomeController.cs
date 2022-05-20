using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TvStore.Models;



namespace TvStore.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        public HomeController(IRepository r)
        {
            repo = r;
        }
        public IActionResult Index()
        {
            return View(repo.GetAll());
        }
        public IActionResult GetTv(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            Tv tv = repo.Get(id.Value);
            if (tv == null)
                return NotFound();
            return View(tv);
        }

        public IActionResult AddTv() => View();

        [HttpPost]
        public IActionResult AddTv(Tv tv)
        {
            if (ModelState.IsValid)
            {
                repo.Create(tv);
                return RedirectToAction("Index");
            }
            return View(tv);
        }
    }
}




//{
//    public class HomeController : Controller
//    {
//        IRepository repo;
//        public HomeController(IRepository r)
//        {
//            repo = r;
//        }
//        public IActionResult Index()
//        {
//            return View(repo.GetAll());
//        }
//        public IActionResult GetUser(int? id)
//        {
//            if (!id.HasValue)
//                return BadRequest();
//            Tv tv = repo.Get(id.Value);
//            if (tv == null)
//                return NotFound();
//            return View(tv);
//        }

//        public IActionResult AddUser() => View();

//        [HttpPost]
//        public IActionResult AddUser(Tv tv)
//        {
//            if (ModelState.IsValid)
//            {
//                repo.Create(tv);
//                return RedirectToAction("Index");
//            }
//            return View(tv);
//        }
//    }
//}

