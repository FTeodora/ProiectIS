namespace ProiectIS.Models

{
    [Serializable]
    public class User
    {

        public long id { get; set; } = -1;
        public string username { get; set; } = null;
        public string password { get; set; } = null;

        public string nume { get; set; } = null;

        public string prenume { get; set; } = null;

        public string email { get; set; } = null;

        public string rol { get; set; } = null;
        public User()
        {

        }
       
        public User(List<Object> source)
        {

            id = (long)source[0];
            username = (string)source[1];
            password = (string)source[2];
            nume = (string)source[3];
            prenume = (string)source[4];
            email = (string)source[5];
            rol = (string)source[6];

        }

        
    }
}
