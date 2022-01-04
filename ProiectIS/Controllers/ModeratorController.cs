using Microsoft.AspNetCore.Mvc;
using ProiectIS.Models;
namespace ProiectIS.Controllers
{
    public class ModeratorController : Controller
    {
        public IActionResult Index()
        {
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> res = db.genericSelect("grup", "*", null);
            db.closeConnection();
            List<DateGrup> groups = new List<DateGrup>();
            foreach (var i in res)
            {
                groups.Add(new DateGrup(i));
            }

            ViewData["Groups"] = groups;
            
            return View();
        }
    }
}
