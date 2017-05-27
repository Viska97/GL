using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лабиринт
{
    public class Student
    {
        public int id;
        public string fio;
        public string login;
        public string password;

        public Student(int id, string fio, string login, string password)
        {
            this.id = id;
            this.fio = fio;
            this.login = login;
            this.password = password;
        }
    }
}
