using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectIS.Models;

namespace ProiectIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Test()
        {
            var db = new Database();
            ViewBag.mesaj = db.test();
            return View();
        }

        [HttpGet]
        public IActionResult Homepage()
        {
            if(HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Home");
            }
            return View();
        }

        public class User
        {
            public string username { get; set; }
            public string password { get; set; }
              
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {
            var db = new Database();
            var res = db.checkUser(user.username, user.password);
            db.closeConnection();
            if(res.CompareTo("-1") == 0) {
                return Ok("Index");
            }

            HttpContext.Session.SetString("id", res);
            return Ok("Homepage");
        }

        [HttpPost]
        public async Task<IActionResult> LogoutUser()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home");
        }
    }
}
