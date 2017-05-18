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
                command = new SQLiteCommand("CREATE TABLE Results (id INTEGER, datetime DATETIME, lgentype INTEGER, lstyle INTEGER, lsize INTEGER, lminutes INTEGER, lseconds INTEGER, optimalcount INTEGER, studentcount INTEGER, minutes INTEGER, seconds INTEGER, result TEXT);", connection);
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
                //connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;Password={1};", databaseName, pass));
                connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
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
            string dt = "";
            SQLiteCommand command = new SQLiteCommand(string.Format("INSERT INTO 'Results' ('id', 'datetime', 'lgentype', 'lstyle', 'lsize', 'lminutes', 'lseconds', 'optimalcount', 'studentcount', 'minutes', 'seconds', 'result') VALUES ({0}, DATETIME('NOW'), {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, '{11}');", id, dt, lgentype, lstyle, lsize, lminutes, lsecondes, optimalcount, studentcount, minutes, seconds, result), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void ResetDatabase()
        {
            File.Create(deleteName);
            Application.Restart();
        }

        
    }
}
