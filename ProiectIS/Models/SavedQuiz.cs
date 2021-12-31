namespace ProiectIS.Models
{

    [Serializable]
    public class SavedQuiz
    {
        public long id { get; set; }
        public long authorID { get; set; }
        public string title { get; set; }
        public List<int> questionsId { get; set; }
        public List<Question> questions { get; set; }
        public SavedQuiz() {  }
        public SavedQuiz(List<Object> list) { id = (long)list[0]; authorID = (long)list[1]; title = (string)list[2]; }
        public void composeQuestionsID(List<Object> items)
        {
            questionsId = new List<int>();
            foreach (var i in items)
                questionsId.Add((int)i);

        }
        public List<Question> databaseQuestions(List<List<Object>> items)
        {
            questions = new List<Question>();
            string conditionString = "(";

            Database db = Database.Instance;
            foreach (var i in questionsId)
            {
                conditionString += questionsId.ToString() + ",";
            }
            List<List<Object>> res = db.genericSelect("questions", "*", "id IN(" + conditionString + ")");
            foreach (var i in res)
                questions.Add(new Question(i));
            return questions;
        }
    }
}
