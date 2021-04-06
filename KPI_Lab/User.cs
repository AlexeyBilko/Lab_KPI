using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI_Lab
{
    abstract public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string name, string surname, string dateOfBirth, string login, string password)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Login = login;
            Password = password;
        }
    }
}
