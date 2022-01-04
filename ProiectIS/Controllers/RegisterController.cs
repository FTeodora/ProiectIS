using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProiectIS.Models;
using System.Security.Cryptography;
using System.Text;

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


            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(user.password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            Console.WriteLine(sb.ToString());
            var res = db.insert(user.username, sb.ToString(), user.nume, user.prenume, user.email, user.rol);
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
