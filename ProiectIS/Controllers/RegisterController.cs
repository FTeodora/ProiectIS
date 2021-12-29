using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProiectIS.Models;

namespace ProiectIS.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public class User
        {
            public string username { get; set; }
            public string password { get; set; }
            public string nume { get; set; }
            public string prenume { get; set; }
            public string email { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {

            var db = new Database();
            var res = db.insert(user.username, user.password,user.nume,user.prenume,user.email);
            db.closeConnection();
            return Ok(res);
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
    }
}
