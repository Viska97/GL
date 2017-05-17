using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;

namespace Лабиринт
{
    public static class SQLHelper
    {
        static string databaseName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "gl.db");
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
                command = new SQLiteCommand("CREATE TABLE Results (id INTEGER, datetime DATETIME, lname TEXT, lgentype INTEGER, lstyle INTEGER, lsize INTEGER, optimalcount INTEGER, studentcount INTEGER, time TIME, result TEXT);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE Presets (presetid INTEGER, lname TEXT, lgentype INTEGER, lstyle INTEGER, lsize INTEGER, minutes INTEGER, seconds INTEGER);", connection);
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
            string[] credentials = new string[3];
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("SELECT * FROM Accounts WHERE id='{0}' LIMIT 1", id), connection);
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                credentials[0] = record["familiya"].ToString();
                credentials[1] = record["imya"].ToString();
                credentials[2] = record["otchestvo"].ToString();
            }
            connection.Close();
            return credentials;
        }

    }
}
