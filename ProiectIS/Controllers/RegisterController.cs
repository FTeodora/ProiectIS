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

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {

            var db = Database.Instance;
            db.openConnection();
            var res = db.insert(user.username, user.password, user.nume, user.prenume, user.email);
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
