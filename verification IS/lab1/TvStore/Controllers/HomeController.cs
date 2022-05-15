using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TvStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace TvStore.Controllers
{
    public class HomeController : Controller
    {
        TvContext db;
        public HomeController(TvContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Tv.ToList());
        }
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.TvId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Order order)
        {
            db.Orders.Add(order);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + order.User + ", за покупку!";
        }
    }
}
