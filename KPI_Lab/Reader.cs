using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI_Lab
{
    public class Reader : User
    {
        public List<Book> books;
        public int fine;

        public Reader(string name, string surname, string dateOfBirth, string login, string password) : base(name,surname,dateOfBirth,login,password)
        {
            fine = 0;
            books = new List<Book>();
        }

        public void CalcFine()
        {
            foreach (var item in books)
            {
                if (item.DateOfIsuue == "expired")
                    fine += 1;
            }
        }

        public string GetString()
        {
            return Name + " " + Surname + " " + DateOfBirth + " " + Login + " " + Password + " " + fine;
        }
    }
}
