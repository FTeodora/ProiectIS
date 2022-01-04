using Microsoft.AspNetCore.Mvc;
using ProiectIS.Models;
namespace ProiectIS.Controllers
{
    public class GroupController : Controller
    {
        //Dictionary<long,NotificationsDispatcher> dispatcher=new Dictionary<long,NotificationsDispatcher>();
        NotificationsDispatcher dispatcher = new NotificationsDispatcher();
        DateGrup group = null;
        public IActionResult Index(long id)
        {
            var grupID = HttpContext.Session.GetString("groupID");
            Console.WriteLine("Selectati studentii din grupul cu id-ul: " + id);
            var db = Database.Instance;
            db.openConnection();
            List<List<Object>> res = db.genericSelect("users g inner join grupmember gl on gl.studentID=g.id", "*", "gl.grupID=" + id);
            db.closeConnection();
            List<User> students = new List<User>();
            foreach (var i in res)
            {
                students.Add(new User(i));
            }
            ViewData["incomingChallenges"] = incomingChallenges(id);
            ViewData["outcomingChallenges"] = outcomingChallenges(id);
            ViewData["students"] = students;
            return getTheGroup(id);
        }
        public IActionResult Challenge(long id)
        {

            return getTheGroup(id);
        }
        public IActionResult Members(long id)
        {
            var db = Database.Instance;
            db.openConnection();
            List<List<Object>> res = db.genericSelect("users g inner join grupmember gl on gl.studentID=g.id", "*", "gl.grupID=" + id);
            db.closeConnection();
            List<User> students = new List<User>();
            foreach (var i in res)
            {
                students.Add(new User(i));
            }
            ViewData["groupMembers"] = students;
            return getTheGroup(id);
        }
        [HttpPost]
        public async void challengeGroup([FromBody] ScheduledQuiz quiz)
        {

            Database db = Database.Instance;

            List<string> fields = new List<string>();
            fields.Add("challengerID");
            fields.Add("challengedID"); ;
            fields.Add("scheduledTime");
            List<Object> vals = new List<Object>();
            vals.Add(quiz.challengedID);
            vals.Add(quiz.challengerID);
            quiz.getName();
            quiz.getChallengedName();
            vals.Add(quiz.scheduledTime.ToString("yyyy-MM-dd HH:mm:ss"));

            db.openConnection();
            db.genericInsert("scheduledMatch", fields, vals);
            db.closeConnection();

            //dispatcher[quiz.challengedID] = new NotificationsDispatcher(this.group);
            dispatcher = new NotificationsDispatcher(this.group);
            DateGrup challengedGroup = new DateGrup();
            challengedGroup.nume = quiz.challengedName;
            challengedGroup.id = quiz.challengedID;
            //dispatcher[quiz.challengerID].Subscribe(challengedGroup);
            dispatcher.Subscribe(challengedGroup);
        }
        [HttpPost]
        public List<DateGrup> searchGroup([FromBody] DateGrup group)
        {
            List<DateGrup> grup = new List<DateGrup>();
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> rez = db.genericSelect("grup", "*", " nume like '%" + group.nume + "%' AND id <> " + group.id);
            db.closeConnection();
            foreach (List<Object> item in rez)
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
            database.closeConnection();
            DateGrup grup;

            if (rez.Count > 0)
                grup = new DateGrup(rez[0]);
            else
                return NotFound();
            this.group = grup;
            ViewData["GroupName"] = grup.nume;
            ViewData["groupID"] = grup.id;
            ViewData["GroupDesc"] = grup.descriere;
            return View();
        }
        [HttpPost]
        public long getMemberAmount([FromBody] DateGrup id)
        {
            return DateGrup.getMemberAmount(id.id);
        }
        [HttpPost]
        public bool myGroupBusy([FromBody] ScheduledQuiz q)
        {
            return ScheduledQuiz.checkBusy(q);
        }
        [HttpPost]
        public bool checkPrerequisites([FromBody] ScheduledQuiz q)
        {

            if (DateGrup.getMemberAmount(q.challengedID) < 3)
                return false;
            if (ScheduledQuiz.checkBusy(q))
                return false;
            return true;
        }

        public List<ScheduledQuiz> incomingChallenges(long id)
        {
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> list = db.genericSelect("scheduledMatch","*","challengedID="+id);
            db.closeConnection();
            List<ScheduledQuiz> scheduledQuizzes = new List<ScheduledQuiz>();
            foreach (var item in list)
            {
                ScheduledQuiz q=new ScheduledQuiz(item);
                q.getName();
                scheduledQuizzes.Add(q);
                
            }
            return scheduledQuizzes;
        }
        public List<ScheduledQuiz> outcomingChallenges(long id)
        {
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> list = db.genericSelect("scheduledMatch", "*", "challengerID=" + id);
            db.closeConnection();
            List<ScheduledQuiz> scheduledQuizzes = new List<ScheduledQuiz>();
            foreach (var item in list)
            {
                ScheduledQuiz q = new ScheduledQuiz(item);
                q.getChallengedName();
                scheduledQuizzes.Add(q);

            }
            return scheduledQuizzes;
        }
    }
}
