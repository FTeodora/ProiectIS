using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectIS.Models;

namespace ProiectIS.Controllers
{
    public class StudentController : Controller
    {
        string idUser;
        public IActionResult Index()
        {
            idUser = HttpContext.Session.GetString("id");
            var db = Database.Instance;
            List<List<Object>> res = db.genericSelect("grup g inner join grupmember gl on g.id=gl.grupID", "*", "gl.studentID=" + idUser);
            List<DateGrup> groups = new List<DateGrup>();
            foreach (var i in res)
            {
                groups.Add(new DateGrup(i));
            }

            ViewData["Groups"] = groups;

            // Console.WriteLine("nr de grupuri " + groups.Count);


            return View();
        }

        public IActionResult Group(long res)
        {
            ViewData["GroupID"] = res;
            ViewData["id"] = HttpContext.Session.GetString("id");

            HttpContext.Session.SetString("GroupID", res.ToString());

            Console.WriteLine("resID2= " + res);
            return View("StudentGroup");
        }

        [HttpPost]
        public IActionResult JoinGroup([FromForm] long groupID)
        {

            idUser = HttpContext.Session.GetString("id");

            var db = Database.Instance;

            Console.WriteLine(idUser + " " + groupID);

            List<string> fields = new List<string>();
            fields.Add("studentID");
            fields.Add("grupID");

            List<Object> values = new List<object>();
            values.Add(idUser);
            values.Add(groupID);

            DateReturn resInsert = db.genericInsert("grupmember", fields, values);

            Console.WriteLine("Studentul a fost adaugat in grupul cu id-ul: " + groupID);

            return View("JoinGroup");
        }

        [HttpPost]
        public IActionResult LeaveGroup([FromForm] long groupID)
        {

            idUser = HttpContext.Session.GetString("id");

            var db = Database.Instance;

            int resDelete = db.genericDelete("grupmember", "grupID=" + groupID + " and studentID=" + idUser);

            Console.WriteLine("Studentul a fost sters din grupul cu id-ul: " + groupID);

            return View("JoinGroup");
        }

        public IActionResult ToJoinGroup()
        {
            return View("JoinGroup");
        }

        public IActionResult ToLeaveGroup()
        {
            return View("LeaveGroup");
        }

        public IActionResult ViewPersonalData()
        {

            ViewData["Email"] = HttpContext.Session.GetString("Email");
            ViewData["Nume"] = HttpContext.Session.GetString("Nume");
            ViewData["Prenume"] = HttpContext.Session.GetString("Prenume");
            ViewData["Rol"] = HttpContext.Session.GetString("Rol");

            return View("StudentProfile");
        }

    }
}
