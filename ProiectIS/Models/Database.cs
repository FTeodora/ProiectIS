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
        public int genericInsert(string tableName,List<string>fieldNames,List<Object> values)
        {
            string fields = "(";
            foreach (var field in fieldNames)
            {
                fields += field+",";
            }
            fields = fields.Remove(fields.Length - 1, 1) + ")";
            string vals = "(";
            foreach(var value in values)
            {
                vals += value.ToString() + ",";
            }
            vals = vals.Remove(vals.Length - 1, 1) + ")";
            var cmd = new MySqlCommand($"insert into "+tableName+fields +
                $" values"+vals, conn);
            var res = cmd.ExecuteNonQuery();

            return res;
        }
        public int genericSelect(string tableName,string fields, String condition)
        {
            MySqlCommand cmd;
            if (condition == null)
            {
                cmd = new MySqlCommand($"select " + fields + " from " + tableName);
            }
            else
            {
                cmd = new MySqlCommand($"select " + fields + " from " + tableName +
                               $" WHERE " + condition, conn);
            }
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
