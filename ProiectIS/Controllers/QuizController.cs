using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectIS.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProiectIS.Hubs;
using System.Threading;
namespace ProiectIS.Controllers
{

    public class QuizController : Controller
    {
        string jsonString;
        int totalScore=0;
        long lobby;
        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Home/Index");
            }

            return View();
        }
        public IActionResult Lobby(long id)
        {
            ViewData["lobbyID"] = id;
            lobby = id;

            return View();
        }
        public IActionResult Challenge()
        {
         
            return View();
        }
        public IActionResult MyQuizes()
        {
            Database db = Database.Instance;
            db.openConnection();
            if (HttpContext.Session.GetString("id") == null)
                return Redirect("/Home/Login");
            List<List<Object>> res = db.genericSelect("savedQuiz", "*", "authorID=" + HttpContext.Session.GetString("id"));
            List<SavedQuiz> saved = new List<SavedQuiz>();
            foreach (var i in res)
            {
                saved.Add(new SavedQuiz(i));
            }
            
            jsonString = JsonSerializer.Serialize(saved);
            ViewData["json"] = jsonString;
            return View();
        }
        public IActionResult MyQuizQuestions(int quizID)
        {
            Database db = Database.Instance;
            db.openConnection();
            if (HttpContext.Session.GetString("id") == null)
                return Redirect("/Home/Login");
            List<List<Object>> res = db.genericSelect("question INNER JOIN quizQuestions ON question.id=quizQuestions.questionID INNER JOIN savedQuiz ON savedQuiz.id=quizQuestions.quizID AND savedQuiz.ID=" + quizID, "*", null);
            List<Question> saved = new List<Question>();
            foreach (var i in res)
            {
                saved.Add(new Question(i));
            }
            jsonString = JsonSerializer.Serialize(saved);
            ViewData["json"] = jsonString;


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
            var db = Database.Instance;
            ViewBag.mesaj = db.test();
            return View();
        }
        [HttpGet]
        public IActionResult Finished(int score)
        {
            ViewData["scor"] = score;
            return View();
        }
        [HttpGet]
        public IActionResult Index(long id)
        {
            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Home/Index");
            }
            lobby = id;
            List<Question> saved;
            if (id == 0)
                saved = generateQuestions(6);
            else
            {
                saved = generateQuestions(6);
            }
            ViewData["scor"] = 0;
            ViewData["lobbyID"] = id;
            jsonString = JsonSerializer.Serialize(saved);
            ViewData["json"] = jsonString;

            return View();
        }
        [HttpPost]
        public List<Question> searchQuestion([FromBody] Question q)
        {
            string condition ="questionSubject LIKE '%"+q.questionSubject+"%' AND enunt LIKE '%"+q.enunt+"%'";
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> res=db.genericSelect("Question","*",condition);
            db.closeConnection();
            List<Question> questions=new List<Question>();
            foreach (List<Object> var in res)
            {
                questions.Add(new Question(var));
            }
            return questions;

        }

        [HttpPost]
        public async Task<IActionResult> insertQuiz([FromBody] SavedQuiz q)
        {
            if (HttpContext.Session.GetString("id") == null)
                return Redirect("/Home/Index");
            Database db = Database.Instance;
            db.openConnection();
            List<String> fields = new List<String>();
            fields.Add("authorID");
            fields.Add( "title");
            List<Object> values=new List<Object>();
           
            values.Add(HttpContext.Session.GetString("id"));
            values.Add(q.title);
            db.openConnection();
            q.id = db.genericInsert("savedQuiz", fields, values).lastID;
            List<String> questionField=new List<String>();
            questionField.Add("quizID");
            questionField.Add("questionID");

            foreach(var question in q.questionsId) {
                List<Object> vals=new List<Object>();
                vals.Add(q.id);
                vals.Add(question);
                db.genericInsert("quizQuestions", questionField,vals);
            }
            return Redirect("/Quiz/MyQuizes");

        }
        public  List<Question> generateQuestions(int amount)
        {
            List<Question> questions = new List<Question>();
            lock (this)
            {
                var db = Database.Instance;
            db.openConnection();
            List<List<Object>> question = db.genericSelect("Question ORDER BY RAND() LIMIT "+amount, "*", null);
           
            foreach (List<Object> var in question)
            {
                questions.Add(new Question(var));
            }
            db.closeConnection();
            }
            return questions;
        }
       

        /*[HttpPost]
        public async void UpdateScore([FromBody] Score s)
        {
           
            totalScore= s.score;
      
        }


   
    public class Score
    {
        public int score { get; set; }


    }*/
    }
    
}
