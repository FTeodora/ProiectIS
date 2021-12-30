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
    public class ProfesorController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ToCreateGroup()
        {
            return View("CreateGroup");
        }

        public IActionResult Group(long res)
        {
            ViewData["GroupID"] = res;
            ViewData["id"] = HttpContext.Session.GetString("id");

            Console.WriteLine("resID2= " + res);
            return View();
        }

        [HttpPost]
        public IActionResult CreateGroup([FromForm] DateGrup date)
        {

            List<string> fields = new List<string>();
            fields.Add("nume");
            fields.Add("descriere");

            List<Object> values = new List<Object>();
            values.Add(date.nume);
            values.Add(date.descriere);

            var db = Database.Instance;

            var res = db.genericInsert("grup", fields, values);

            Console.WriteLine("buna");

            var resID = res.lastID;

            Console.WriteLine("resID= " + resID);

            fields.Clear();
            fields.Add("profesorID");
            fields.Add("grupID");

            values.Clear();
            values.Add(HttpContext.Session.GetString("id"));
            values.Add(res.lastID);

            res = db.genericInsert("grupleader", fields, values);

            Console.WriteLine("seara");

            return RedirectToAction("Group", new { res = resID });
        }

        public class DateGrup
        {
            public string nume { get; set; }
            public string descriere { get; set; }
        }
    }
}
