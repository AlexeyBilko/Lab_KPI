using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI_Lab
{
    public class Admin : Librarian
    {
        public List<Librarian> librarians;
        public List<Admin> admins;

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

        //public void ReadAdmins(string path)
        //{
        //    List<string> tmp = File.ReadAllLines(path).ToList();

        //    for (int i = 0; i < tmp.Count; i++)
        //    {
        //        string[] buf = tmp[i].Split(' ');
        //        admins.Add(new Admin(readers, books, librarians, admins, buf[0], buf[1], buf[2], buf[3], buf[4]));
        //    }
        //}

        public void SaveLibrariansChangesInFile(string path)
        {
            File.WriteAllText(path, "");

            List<string> tmp = new List<string>();

            for (int i = 0; i < librarians.Count; i++)
            {
                tmp.Add(librarians[i].GetString());
            }

            File.AppendAllLines(path, tmp);
        }

        public void SaveAdminsChangesInFile(string path)
        {
            File.WriteAllText(path, "");

            List<string> tmp = new List<string>();

            for (int i = 0; i < admins.Count; i++)
            {
                tmp.Add(admins[i].GetString());
            }

            File.AppendAllLines(path, tmp);
        }
    }
}
