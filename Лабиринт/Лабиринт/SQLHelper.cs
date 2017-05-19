using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;
using System.Windows.Forms;

namespace Лабиринт
{
    public static class SQLHelper
    {
        static string databaseName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "gl.db");
        static string deleteName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "deleteme.txt");
        static string pass = "kW82OT9uM5";
        static SQLiteConnection connection;
        static SQLiteDataReader reader;



        public static void CheckDatabase()
        {
            if (!File.Exists(databaseName))
            {
                SQLiteConnection.CreateFile(databaseName);
                connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
                //connection.SetPassword(pass);
                SQLiteCommand command = new SQLiteCommand("CREATE TABLE Accounts (id INTEGER PRIMARY KEY, login TEXT, password TEXT, familiya TEXT, imya TEXT, otchestvo TEXT, isteacher BOOLEAN);", connection);
                connection.Open();
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE Results (id INTEGER, datetime TEXT, lgentype INTEGER, lstyle INTEGER, lsize INTEGER, lminutes INTEGER, lseconds INTEGER, optimalcount INTEGER, studentcount INTEGER, minutes INTEGER, seconds INTEGER, result TEXT);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE Presets (presetid INTEGER, lname TEXT, lgentype INTEGER, lstyle INTEGER, lsize INTEGER, minutes INTEGER, seconds INTEGER);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (0, 'Лабиринт 1', 0, 0, 21, 1, 0);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (1, 'Лабиринт 2', 0, 0, 21, 1, 0);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (2, 'Лабиринт 3', 0, 0, 21, 1, 0);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (3, 'Лабиринт 4', 0, 0, 21, 1, 0);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (4, 'Лабиринт 5', 0, 0, 21, 1, 0);", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                int tables = 0;
                //connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;Password={1};", databaseName, pass));
                connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;", connection);
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    tables++;
                }
                if (tables == 0)
                {
                    command = new SQLiteCommand("CREATE TABLE Accounts (id INTEGER PRIMARY KEY, login TEXT, password TEXT, familiya TEXT, imya TEXT, otchestvo TEXT, isteacher BOOLEAN);", connection);
                    command.ExecuteNonQuery();
                    command = new SQLiteCommand("CREATE TABLE Results (id INTEGER, datetime TEXT, lgentype INTEGER, lstyle INTEGER, lsize INTEGER, lminutes INTEGER, lseconds INTEGER, optimalcount INTEGER, studentcount INTEGER, minutes INTEGER, seconds INTEGER, result TEXT);", connection);
                    command.ExecuteNonQuery();
                    command = new SQLiteCommand("CREATE TABLE Presets (presetid INTEGER, lname TEXT, lgentype INTEGER, lstyle INTEGER, lsize INTEGER, minutes INTEGER, seconds INTEGER);", connection);
                    command.ExecuteNonQuery();
                    command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (0, 'Лабиринт 1', 0, 0, 21, 1, 0);", connection);
                    command.ExecuteNonQuery();
                    command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (1, 'Лабиринт 2', 0, 0, 21, 1, 0);", connection);
                    command.ExecuteNonQuery();
                    command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (2, 'Лабиринт 3', 0, 0, 21, 1, 0);", connection);
                    command.ExecuteNonQuery();
                    command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (3, 'Лабиринт 4', 0, 0, 21, 1, 0);", connection);
                    command.ExecuteNonQuery();
                    command = new SQLiteCommand("INSERT INTO 'Presets' ('presetid', 'lname', 'lgentype', 'lstyle', 'lsize', 'minutes', 'seconds') VALUES (4, 'Лабиринт 5', 0, 0, 21, 1, 0);", connection);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static bool CheckTeacherRegistration()
        {
            bool flag=false;
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Accounts WHERE isteacher=1 LIMIT 1", connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                if (record["isteacher"].ToString() == "True")
                {
                    flag = true;
                }
            }
            connection.Close();
            return flag;  
        }

        public static string AddAccount(string loginfamiliya, string password, string imya, string familiya, string otchestvo, int isteacher)
        {
            int maxid=10;
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Accounts ORDER BY id DESC LIMIT 1", connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                maxid = Convert.ToInt32(record["id"].ToString());
                maxid++;
            }
            string login = loginfamiliya + Convert.ToString(maxid);
            command = new SQLiteCommand(string.Format("INSERT INTO 'Accounts' ('id', 'login', 'password', 'familiya', 'imya', 'otchestvo', 'isteacher') VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6});", maxid, login, password, imya, familiya,otchestvo,isteacher), connection);
            command.ExecuteNonQuery();
            connection.Close();
            return login;
        }

        public static int Authorize(string login, string password)
        {
            bool islogin = false;
            int resultcode = 0;
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("SELECT * FROM Accounts WHERE login='{0}' LIMIT 1",login), connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                islogin = true;
                if (record["password"].ToString() == password)
                {
                    resultcode = Convert.ToInt32(record["id"].ToString());
                }
                else
                {
                    resultcode = 2;
                }
            }
            if (!islogin)
            {
                resultcode = 1;
            }
            connection.Close();
            return resultcode;
        }

        public static string[] GetCredentials(int id)
        {
            string[] credentials = new string[4];
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("SELECT * FROM Accounts WHERE id='{0}' LIMIT 1", id), connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                credentials[0] = record["familiya"].ToString();
                credentials[1] = record["imya"].ToString();
                credentials[2] = record["otchestvo"].ToString();
                credentials[3] = record["isteacher"].ToString();
            }
            connection.Close();
            return credentials;
        }

        public static List<string> GetProfilesNames()
        {
            List<string> presetsnames = new List<string>();
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Presets ORDER BY presetid", connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                presetsnames.Add(record["lname"].ToString());
            }
            connection.Close();
            return presetsnames;
        }

        public static string[] GetProfileParameters(int presetid)
        {
            string[] parameters = new string[6];
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("SELECT * FROM Presets WHERE presetid='{0}' LIMIT 1", presetid), connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                parameters[0] = record["lname"].ToString();
                parameters[1] = record["lgentype"].ToString();
                parameters[2] = record["lstyle"].ToString();
                parameters[3] = record["lsize"].ToString();
                parameters[4] = record["minutes"].ToString();
                parameters[5] = record["seconds"].ToString();
            }
            connection.Close();
            return parameters;
        }

        public static void SetProfileParameters(int presetid, string lgentype, string lstyle, string lsize, string minutes, string seconds)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("UPDATE Presets SET lgentype = '{0}', lstyle = '{1}', lsize = '{2}', minutes = '{3}', seconds = '{4}' WHERE presetid='{5}'", lgentype, lstyle, lsize, minutes, seconds, presetid), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void AddResult(int id, int lgentype, int lstyle, int lsize, int lminutes, int lsecondes, int optimalcount, int studentcount, int minutes, int seconds, string result)
        {
            connection.Open();
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SQLiteCommand command = new SQLiteCommand(string.Format("INSERT INTO 'Results' ('id', 'datetime', 'lgentype', 'lstyle', 'lsize', 'lminutes', 'lseconds', 'optimalcount', 'studentcount', 'minutes', 'seconds', 'result') VALUES ({0}, '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, '{11}');", id, dt, lgentype, lstyle, lsize, lminutes, lsecondes, optimalcount, studentcount, minutes, seconds, result), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static List<ListViewItem> GetResults(int id)
        {
            List<ListViewItem> results = new List<ListViewItem>();
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("SELECT * FROM Results WHERE id='{0}' ORDER BY datetime DESC", id), connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                ListViewItem lvi = new ListViewItem(record["datetime"].ToString());
                switch (Convert.ToInt32(record["lgentype"].ToString()))
                {
                    case 0:
                        lvi.SubItems.Add("Поиск в глубину");
                        break;
                    case 1:
                        lvi.SubItems.Add("Рекурсивный откат");
                        break;
                    
                }
                switch (Convert.ToInt32(record["lstyle"].ToString()))
                {
                    case 0:
                        lvi.SubItems.Add("Классический");
                        break;
                    case 1:
                        lvi.SubItems.Add("Гибридный 1");
                        break;
                    case 2:
                        lvi.SubItems.Add("Гибридный 2");
                        break;
                }
                int lsize = Convert.ToInt32(record["lsize"].ToString());
                string lminutes = record["lminutes"].ToString();
                if (Convert.ToInt32(lminutes) <10)
                {
                    lminutes = "0" + lminutes;
                }
                string lseconds = record["lseconds"].ToString();
                if (Convert.ToInt32(lseconds) < 10)
                {
                    lseconds = "0" + lseconds;
                }
                string optimalcount = record["optimalcount"].ToString() + " кл";
                string studentcount = record["studentcount"].ToString() + " кл";
                int minutes = Convert.ToInt32(record["minutes"].ToString());
                int seconds = Convert.ToInt32(record["seconds"].ToString());
                int totalminutes = Convert.ToInt32(lminutes) - minutes;
                int totalseconds = Convert.ToInt32(lseconds) - seconds;
                if (totalseconds < 0)
                {
                    totalseconds =  60 + totalseconds;
                    totalminutes=totalminutes-1;
                }
                string tminutes = Convert.ToString(totalminutes);
                string tseconds = Convert.ToString(totalseconds);
                if (totalminutes < 10)
                {
                    tminutes = "0" + tminutes;
                }
                if (totalseconds < 10)
                {
                    tseconds = "0" + tseconds;
                }
                lvi.SubItems.Add(string.Format("{0}x{0}",lsize));
                lvi.SubItems.Add(string.Format("{0}:{1}", lminutes, lseconds));
                lvi.SubItems.Add(optimalcount);
                lvi.SubItems.Add(studentcount);
                lvi.SubItems.Add(string.Format("{0}:{1}", tminutes, tseconds));
                lvi.SubItems.Add(record["result"].ToString());
                results.Add(lvi);
            }
            connection.Close();
            return results;
        }

        public static List<Student> GetAccounts()
        {
            List<Student> students = new List<Student>();
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Accounts WHERE isteacher=0 ORDER BY id", connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                string familiya = record["familiya"].ToString();
                string imya = record["imya"].ToString();
                string otchestvo = record["otchestvo"].ToString();
                string fio = string.Format("{0} {1} {2}", familiya, imya, otchestvo);
                Student student = new Student(Convert.ToInt32(record["id"].ToString()), fio);
                students.Add(student);
            }
            connection.Close();
            return students;
        }

        public static void DeleteResults(int id)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("DELETE FROM Results WHERE id='{0}'", id), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateTempResult(int id, int lgentype, int lstyle, int lsize, int lminutes, int lsecondes, int optimalcount, int studentcount, int minutes, int seconds, string result)
        {
            string datetime="";
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Results ORDER BY datetime DESC LIMIT 1", connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                datetime = record["datetime"].ToString();
            }
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            command = new SQLiteCommand(string.Format("UPDATE Results SET 'id' = {0}, 'datetime' = '{1}', 'lgentype' = {2}, 'lstyle' = {3}, 'lsize' = {4}, 'lminutes' = {5}, 'lseconds' = {6}, 'optimalcount' = {7}, 'studentcount' = {8}, 'minutes' = {9}, 'seconds' = {10}, 'result' = '{11}' WHERE datetime='{12}'", id, dt, lgentype, lstyle, lsize, lminutes, lsecondes, optimalcount, studentcount, minutes, seconds, result,datetime), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteTempResult()
        {
            string datetime = "";
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Results ORDER BY datetime DESC LIMIT 1", connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                datetime = record["datetime"].ToString();
            }
            string test = datetime;
            command = new SQLiteCommand(string.Format("DELETE FROM Results WHERE datetime='{0}'",datetime), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void ResetDatabaseTables()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("DROP Table Accounts", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DROP Table 'Results'", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DROP Table 'Presets'", connection);
            command.ExecuteNonQuery();
            connection.Close();
            Application.Restart();
        }


    }
}
