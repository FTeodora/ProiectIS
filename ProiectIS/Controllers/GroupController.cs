using Microsoft.AspNetCore.Mvc;
using ProiectIS.Models;
namespace ProiectIS.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index(long id)
        {
            return getTheGroup(id);
        }
        public IActionResult Challenge(long id)
        {

            return getTheGroup(id);
        }

        public IActionResult ToChallenge(long id)
        {

            return View("Challenge");
        }

        [HttpPost]
        public List<DateGrup> searchGroup(string nume)
        {
            List<DateGrup> grup=new List<DateGrup>();
            Database db=Database.Instance;
            db.openConnection();
            List<List<Object>> rez = db.genericSelect("grup", "*", " nume like '%" + nume + "%' AND id <> "+ViewData["GroupID"]);
            foreach(List<Object> item in rez)
            {
                grup.Add(new DateGrup(item));
            }
            return grup;
        }
        public IActionResult getTheGroup(long id)
        {
            Database database = Database.Instance;
            database.openConnection();
            List<List<Object>> rez = database.genericSelect("grup", "*", "id=" + id);
            DateGrup grup;
            database.closeConnection();
            if (rez.Count > 0)
                grup = new DateGrup(rez[0]);
            else
                return NotFound();
            ViewData["GroupName"] = grup.nume;
            ViewData["GroupID"] = grup.id;
            return View();
        }
        
    }
}
