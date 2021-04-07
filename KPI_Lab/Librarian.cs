using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI_Lab
{
    public class Librarian : User
    {
        public List<Reader> readers;
        public List<Book> books;

        public Librarian(List<Reader> readers, List<Book> books, string name, string surname, string dateOfBirth, string login, string password) : base(name, surname, dateOfBirth, login, password)
        {
            this.readers = readers;
            this.books = books;
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void DeleteBook(Book book)
        {
            books.Remove(book);
        }

        public Book Search(int id)
        {
            Book tmp = new Book();

            foreach (var item in books)
            {
                if (item.Id == id)
                    tmp = item;
            }

            if (tmp.Title == "")
                return null;
            return tmp;
        }

        public void GiveBook(Book b, Reader r)
        {
            r.books.Add(b);
        }

        public void AddReader(Reader r)
        {
            readers.Add(r);
        }

        public void RemoveReader(Reader r)
        {
            readers.Remove(r);
        }

        public void ReadReaders(string path)
        {
            List<string> tmp = File.ReadAllLines(path).ToList();

            readers.Clear();

            for (int i = 0; i < tmp.Count; i++)
            {
                string[] buf = tmp[i].Split(' ');
                readers.Add(new Reader(buf[0], buf[1], buf[2], buf[3], buf[4]));
                List<int> id = new List<int>();

                readers[i].fine = Convert.ToInt32(buf[5]);

                if (buf[6] != null)
                {
                    for (int j = 7; j < Convert.ToInt32(buf[6]) + 7; j++)
                    {
                        id.Add(Convert.ToInt32(buf[j]));
                    }

                    for (int d = 0; d < id.Count; d++)
                    {
                        readers[i].books.Add(books.Find(x => x.Id == id[d]));
                    }
                }
            }
        }

        public void SaveReadersChangesInFile(string path)
        {
            File.WriteAllText(path, "");

            List<string> tmp = new List<string>();

            for (int i = 0; i < readers.Count; i++)
            {
                string buf = "";
                foreach (var item in readers[i].books)
                {
                    buf = buf + item.Id.ToString() + " ";
                }

                if(buf == " ")
                    tmp.Add(readers[i].GetString() + " " + 0);
                else
                    tmp.Add(readers[i].GetString() + " " + readers[i].books.Count + " " + buf);
            }

            File.AppendAllLines(path, tmp);
        }

        //тоже самое для book
    }
}
