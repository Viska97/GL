using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лабиринт
{
    class Teacher
    {
        public string familiya, imya, otchestvo, login, password;

        public Teacher()
        { 
        }

        public Teacher(string familiya, string imya, string otchestvo, string login, string password)
        {
            this.familiya = familiya;
            this.imya = imya;
            this.otchestvo = otchestvo;
            this.login = login;
            this.password = password;
        }
    }
}
