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
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Test()
        {
            var db = new Database();
            ViewBag.mesaj = db.test();
            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Quiz");
            }
            return View();
        }

    }
}
