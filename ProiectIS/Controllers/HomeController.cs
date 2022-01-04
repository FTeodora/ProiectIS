using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectIS.Models;
using System.Web;
using System.Security.Cryptography;
using System.Text;

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
            var db = Database.Instance;
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


        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginClass user)
        {
            Console.WriteLine("Inceput");
            var db = Database.Instance;
            db.openConnection();

            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(user.password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            Console.WriteLine("Parola refacuta?: " + sb.ToString());


            List<List<Object>> utilizator = db.genericSelect("users", "*", " username='" + user.username + "' and pass='" + sb + "' or ((rol='MODERATOR' or rol='ADMIN') and pass='" + user.password + "');");

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
            HttpContext.Session.SetString("Email", res.email);

            if (res.rol.CompareTo("PROFESOR") == 0)
                return Ok("/Profesor");
            if (res.rol.CompareTo("STUDENT") == 0)
                return Ok("/Student");
            if (res.rol.CompareTo("MODERATOR") == 0)
                return Ok("/Moderator");
            else return Ok("/Admin");
            return Ok("Homepage");

        }

        [HttpGet]
        public async Task<IActionResult> LogoutUser()
        {
            HttpContext.Session.Clear();
            var db = Database.Instance;
            db.closeConnection();
            return Redirect("/Home");
        }
        private void updateCurrentPassWords()
		{

            Database db = Database.Instance;
            db.closeConnection();
            db.openConnection();
            List<List<Object>> rez = db.genericSelect("users", "*", null);
            db.closeConnection();
            foreach(var i in rez)
			{
                db.openConnection();
                db.closeConnection();
			}
		}
    }
}


