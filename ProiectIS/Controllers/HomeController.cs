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

        List<User> users = new List<User>();
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
            var db = new Database();
            List<List<Object>> user = db.genericSelect("users", "*", null);
            db.closeConnection();
            List<User> users = new List<User>();

            foreach (List<Object> var in user)
            {
                users.Add(new User(var));
            }
            ViewData["users"] = users;

            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Home");
            }


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

        [HttpGet] // *
        public IActionResult Homepage()
        {
            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Home");
            }
            return View();
        }



        public class User
        {

            public long id { get; set; } = -1;
            public string username { get; set; } = null;
            public string password { get; set; } = null;

            public string nume { get; set; } = null;

            public string prenume { get; set; } = null;

            public string email { get; set; } = null;

            public string rol { get; set; } = null;


            public User(List<Object> source)
            {

                id = (long)source[0];
                username = (string)source[1];
                password = (string)source[2];
                nume = (string)source[3];
                prenume = (string)source[4];
                email = (string)source[5];
                rol = (string)source[6];

            }

        }


        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {

            var res = users.Find(x => x.username == user.username && x.password == user.password);

            if (res.id.CompareTo("-1") == 0)
            {
                return Ok("Quiz");
            }

            // HttpContext.Session.SetString("id", res.id);

            if (res.rol.CompareTo("profesor") == 0)
                return Ok("Homepage");
            else
                return Ok("Student");
        }

        [HttpPost]
        public async Task<IActionResult> LogoutUser()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home");
        }
    }
}


