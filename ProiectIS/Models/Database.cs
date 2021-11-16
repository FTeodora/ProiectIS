// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;

namespace ProiectIS.Models
{
    public class Database
    {
        MySqlConnection conn;
        public Database()
        {
            conn = new MySqlConnection("Server=127.0.0.1;Database=kohaat;Uid=root;Pwd=root;");
            conn.Open();
        }

        public String test()
        {
            var cmd = new MySqlCommand("select * from users", conn);
            var res = cmd.ExecuteScalar().ToString();

            return res;
        }

        public int insert(string uname, string pass, string nume, string prenume, string eMail)
        {
            var cmd = new MySqlCommand($"insert into users(username,pass,nume,prenume,eMail,rol)" +
                $" values( '{uname}','{pass}','{nume}','{prenume}','{eMail}','STUDENT')", conn);
            var res = cmd.ExecuteNonQuery();

            return res;
        }

        public String checkUser(string uname, string pass)
        {
            var cmd = new MySqlCommand($"select * from users where username='{uname}' and pass='{pass}'", conn);
            var res = cmd.ExecuteScalar()?.ToString();
            if (res == null) res = "-1";

            return res;
        }

    }
}
