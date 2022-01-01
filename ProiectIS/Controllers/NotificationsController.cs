using Microsoft.AspNetCore.Mvc;
using ProiectIS.Models;
using System.Text.Json;

namespace ProiectIS.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("id") == null)
                return Redirect("Home/Index");
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> notif=db.genericSelect("notification","*","recipientID="+HttpContext.Session.GetString("id"));
            List<Notification> notifs = new List<Notification>();
            foreach(List<Object> obj in notif)
            {
                notifs.Add(new Notification(obj));
            }
            ViewData["json"] = JsonSerializer.Serialize(notifs);
            return View();
        }
        [HttpPost]
        public List<User> searchUsersFromNotifs([FromBody] User q)
        {
            string condition = "username LIKE '%" + q.username+ "%' AND rol='STUDENT' AND id <> "+HttpContext.Session.GetString("id");
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> res = db.genericSelect("users", "*", condition);
            db.closeConnection();
            List<User> users = new List<User>();
            foreach (List<Object> var in res)
            {
                users.Add(new User(var));
            }
            return users;

        }
        [HttpPost]
        public async Task<IActionResult> deleteNotification([FromBody]Notification n)
        {
           Database db=Database.Instance;
            db.openConnection();
            db.genericDelete("notification", "id=" + n.id);
            return Ok("Quiz/Lobby?id=" + n.lobbyID);
        }
        [HttpPost]
        public long challengeUser([FromBody]User u)
        {
            
            List<String> fieldNames=new List<String>();
            fieldNames.Add("recipientID");
            fieldNames.Add("senderID");
            fieldNames.Add("message");
            fieldNames.Add("accepted");
            fieldNames.Add("declined");
            fieldNames.Add("lobbyID");
            List<Object> fieldValues=new List<Object>();
            
            fieldValues.Add(u.id);
            Console.WriteLine(u.id);
            fieldValues.Add(HttpContext.Session.GetString("id"));
            fieldValues.Add("Ai fost provocat la un meci de catre " + HttpContext.Session.GetString("Nume"));
            fieldValues.Add(0);
            fieldValues.Add(0);
            long generatedLobby = new Random().NextInt64();
            fieldValues.Add(generatedLobby);
           
            Database db = Database.Instance;
            
            db.openConnection();
            
            db.genericInsert("notification",fieldNames,fieldValues);
            db.closeConnection();
            return generatedLobby;
        }
       
    }
}
