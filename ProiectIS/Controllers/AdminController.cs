using Microsoft.AspNetCore.Mvc;
using ProiectIS.Models;

namespace ProiectIS.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitUserId([FromForm] string id)
        {
            HttpContext.Session.SetString("user_id", id);
            return View("Index");
        }

        [HttpPost]
        public IActionResult EditUsername([FromForm] string username)
        {
            var userID = HttpContext.Session.GetString("user_id");
            Database db = Database.Instance;

            var res = db.genericUpdate("users", "username", username, "id=" + userID);

            return View("Index");
        }

        [HttpPost]
        public IActionResult EditPassword([FromForm] string pass)
        {
            var userID = HttpContext.Session.GetString("user_id");
            Database db = Database.Instance;

            var res = db.genericUpdate("users", "pass", pass, "id=" + userID);

            return View("Index");
        }

        [HttpPost]
        public IActionResult EditNume([FromForm] string nume)
        {
            var userID = HttpContext.Session.GetString("user_id");
            Database db = Database.Instance;

            var res = db.genericUpdate("users", "nume", nume, "id=" + userID);

            return View("Index");
        }

        [HttpPost]
        public IActionResult EditPrenume([FromForm] string prenume)
        {
            var userID = HttpContext.Session.GetString("user_id");
            Database db = Database.Instance;

            var res = db.genericUpdate("users", "prenume", prenume, "id=" + userID);

            return View("Index");
        }

        [HttpPost]
        public IActionResult EditEmail([FromForm] string email)
        {
            var userID = HttpContext.Session.GetString("user_id");
            Database db = Database.Instance;

            var res = db.genericUpdate("users", "eMail", email, "id=" + userID);

            return View("Index");
        }
    }
}
