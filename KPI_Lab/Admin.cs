using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI_Lab
{
    public class Admin : Librarian
    {
        List<Librarian> librarians;
        List<Admin> admins;

        public Admin(List<Reader> readers, List<Book> books, List<Librarian> librarians, List<Admin> admins, 
            string name, string surname, string dateOfBirth, string login, string password) : 
            base(readers, books, name, surname, dateOfBirth, login, password)
        {
            this.librarians = librarians;
            this.admins = admins;
        }

        public void AddAdmin(Admin admin)
        {
            admins.Add(admin);
        }
        public void RemoveAdmin(Admin admin)
        {
            admins.Remove(admin);
        }
        public void AddLibrarian(Librarian librarian)
        {
            librarians.Add(librarian);
        }
        public void RemoveLibrarian(Librarian librarian)
        {
            librarians.Remove(librarian);
        }
    }
}
