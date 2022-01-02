// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;
using System.Data;
namespace ProiectIS.Models
{
    public class Database
    {
        MySqlConnection conn;

        private static Database? instance;

        public static Database Instance
        {
            get
            {
                if (Database.instance == null)
                    Database.instance = new Database();
                return Database.instance;
            }

        }
        private Database()
        {
            conn = new MySqlConnection("Server=127.0.0.1;Database=kohaat;Uid=root;Pwd=root;");
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


        public DateReturn genericInsert(string tableName, List<string> fieldNames, List<Object> values)
        {
            DateReturn ret = new DateReturn();

            string fields = "(";
            foreach (var field in fieldNames)
            {
                fields += field + ",";
            }
            fields = fields.Remove(fields.Length - 1, 1) + ")";
            string vals = "(";
            foreach (var value in values)
            {
                vals += "'" + value.ToString() + "'" + ",";
            }
            vals = vals.Remove(vals.Length - 1, 1) + ")";
            Console.WriteLine("insert into " + tableName + fields +
                $" values" + vals);
            var cmd = new MySqlCommand($"insert into " + tableName + fields +
                $" values" + vals, conn);
            var res = cmd.ExecuteNonQuery();

            ret.noRows = res;
            ret.lastID = cmd.LastInsertedId;


            return ret;
        }


        public List<List<Object>> genericSelect(string tableName, string fields, String condition)
        {
            MySqlCommand cmd;
            if (condition == null)
            {
                Console.WriteLine("select " + fields + " from " + tableName);
                cmd = new MySqlCommand($"select " + fields + " from " + tableName, conn);

            }
            else
            {
                Console.WriteLine("select " + fields + " from " + tableName +
                               $" WHERE " + condition);
                cmd = new MySqlCommand($"select " + fields + " from " + tableName +
                               $" WHERE " + condition, conn);

            }
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<List<Object>> values = new List<List<Object>>();
            while (rdr.Read())
            {
                List<Object> columnVals = new List<Object>();
                for (int i = 0; i < rdr.FieldCount; i++)
                    columnVals.Add(rdr.GetValue(i));
                values.Add(columnVals);
            }
            rdr.Close();
            return values;
        }
        public String checkUser(string uname, string pass)
        {
            var cmd = new MySqlCommand($"select * from users where username='{uname}' and pass='{pass}'", conn);
            var res = cmd.ExecuteScalar()?.ToString();
            if (res == null) res = "-1";

            return res;
        }
        public void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        public void openConnection()

        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }
    }
}
