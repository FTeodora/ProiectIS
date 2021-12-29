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
        List<Question> questions = new List<Question>();
        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
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
        public IActionResult Index()
        {
            var db = new Database();
            List<List<Object>> question = db.genericSelect("Question ORDER BY RAND() LIMIT 6", "*", null);
            db.closeConnection();
            List<Question> questions = new List<Question>();
            foreach (List<Object> var in question)
            {
                questions.Add(new Question(var));
            }
            ViewData["questions"] = questions;
            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Quiz");
            }

            return View();
        }


    }
    public class Question
    {

        public long id { get; set; }
        public long authorID { get; set; }
        public string questionSubject { get; set; }
        public string enunt { get; set; }
        public sbyte timeout { get; set; }

        public List<String> options { get; set; }
        public int correctAnsw { get; set; }
        public bool approved { get; set; }
        public Question(List<Object> source)
        {

            id = (long)source[0];
            authorID = (long)source[1];
            questionSubject = (string)source[2];
            enunt = (string)source[3];
            timeout = (sbyte)source[4];
            correctAnsw = (new Random().Next()) % 4;
            options = new List<string>();
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                if (i == correctAnsw)
                {
                    options.Add((string)source[5]);
                }
                else
                {
                    options.Add((string)source[6 + j]);
                    j++;
                }
            }
            approved = (bool)source[9];

        }

    }
}
