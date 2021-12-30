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

namespace ProiectIS.Controllers
{

    public class QuizController : Controller
    {
        string jsonString;
        int totalScore=0;
        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult MyQuizes()
        {
            Database db = Database.Instance;
            if (HttpContext.Session.GetString("id") == null)
                return Redirect("Home/Login");
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
            if (HttpContext.Session.GetString("id") == null)
                return Redirect("Home/Login");
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
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("id") == null)
            {
                return Redirect("/Quiz");
            }
            var db = Database.Instance;
            List<List<Object>> question = db.genericSelect("Question ORDER BY RAND() LIMIT 6", "*", null);
            List<Question> questions = new List<Question>();
            foreach (List<Object> var in question)
            {
                questions.Add(new Question(var));
            }
            ViewData["questions"] = questions;
            ViewData["scor"] = 0;

            jsonString = JsonSerializer.Serialize(questions);
            ViewData["json"] = jsonString;

            return View();
        }
        [HttpPost]
        public List<Question> searchQuestion([FromBody] Question q)
        {
            string condition ="questionSubject LIKE '%"+q.questionSubject+"%' AND enunt LIKE '%"+q.enunt+"%'";
            Database db = Database.Instance;
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
            Database db = Database.Instance;
            List<String> fields = new List<String>();
            fields.Add("authorID");
            fields.Add( "title");
            List<Object> values=new List<Object>();
            values.Add(HttpContext.Session.GetString("id"));
            values.Add(q.title);
            q.id = db.genericInsertReturnLast("savedQuiz", fields, values);
            List<String> questionField=new List<String>();
            questionField.Add("quizID");
            questionField.Add("questionID");

            foreach(var question in q.questionsId) {
                List<Object> vals=new List<Object>();
                vals.Add(q.id);
                vals.Add(question);
                db.genericInsert("quizQuestions", questionField,vals);
            }
            return Redirect("/Home/Homepage");

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
    [Serializable]
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
        public Question()
        {
           
        }
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
    [Serializable]
    public class SavedQuiz
    {
        public long id { get; set; }
        public long authorID { get; set; }   
        public string title { get; set; } 
        public List<int> questionsId { get; set; }
        public List<Question> questions { get; set; }
        public SavedQuiz() { questions= new List<Question>(); }
        public SavedQuiz(List<Object> list) { id =(long) list[0]; authorID = (long)list[1]; title = (string)list[2]; }
        public void composeQuestionsID(List<Object> items)
        {
            questionsId= new List<int>();
            foreach (var i in items)
                questionsId.Add((int)i);

        }
        public List<Question> databaseQuestions(List<List<Object>> items)
        {
            questions= new List<Question>();
            string conditionString="(";
           
            Database db = Database.Instance;
            foreach(var i in questionsId)
            {
                conditionString += questionsId.ToString() + ",";
            }
            List<List<Object>> res = db.genericSelect("questions", "*", "id IN(" + conditionString + ")");
            foreach(var i in res)
                questions.Add(new Question(i)) ;
            return questions;
        }
    }
}
