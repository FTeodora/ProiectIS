﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectIS.Models;
using System.Web;

namespace ProiectIS.Controllers
{
    public class HomeController : Controller
    {

        List<User> users = new List<User>();
        // User currentUser = new User();
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



            //ViewData["users"] = users[0];

            if (HttpContext.Session.GetString("id") != null)
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

        public class UserLoginClass
        {
            public string username { get; set; }
            public string password { get; set; }
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
        public async Task<IActionResult> LoginUser([FromBody] UserLoginClass user)
        {
            Console.WriteLine("Inceput");
            var db = new Database();
            List<List<Object>> utilizator = db.genericSelect("users", "*", " username='" + user.username + "' and pass='" + user.password + "';");
            db.closeConnection();
            Console.WriteLine("SF");

            List<User> users = new List<User>();

            foreach (List<Object> var in utilizator)
            {
                users.Add(new User(var));
            }

            var res = users[0];

            Console.WriteLine(res);

            if (res.id.CompareTo(-1) == 0)
            {
                return Ok("Homepage");
            }

            HttpContext.Session.SetString("id", res.id.ToString());
            HttpContext.Session.SetString("Nume", res.nume);
            HttpContext.Session.SetString("Prenume", res.prenume);
            HttpContext.Session.SetString("Rol", res.rol);

            if (res.rol.CompareTo("PROFESOR") == 0)
                return Ok("Homepage");
            else
                return Ok("/Student");

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


