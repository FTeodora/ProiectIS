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

        public IActionResult Index()
        {
            return View();
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
