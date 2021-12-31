namespace ProiectIS.Models
{
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
}
