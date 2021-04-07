using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI_Lab
{
    public partial class Form1 : Form
    {
        public List<Book> books;
        public List<Reader> readers;
        public List<Librarian> librarians;
        public List<Admin> admins;

        public Form1()
        {
            InitializeComponent();

            books = new List<Book>();
            readers = new List<Reader>();
            librarians = new List<Librarian>();
            admins = new List<Admin>();

            GetBooks();
            GetReaders();
            GetLibrarians();
            GetAdmins();

            comboBox1.Items.Add("admin");
            comboBox1.Items.Add("reader");
            comboBox1.Items.Add("librarian");
        }

        public void GetBooks()
        {
            List<string> tmp = File.ReadAllLines("books.txt").ToList();

            books.Clear();

            for (int i = 0; i < tmp.Count; i++)
            {
                string[] buf = tmp[i].Split(' ');
                books.Add(new Book(buf[0], Convert.ToInt32(buf[1]), buf[2]));
            }
        }

        public void GetReaders()
        {
            List<string> tmp = File.ReadAllLines("readers.txt").ToList();

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

        public void GetLibrarians()
        {
            List<string> tmp = File.ReadAllLines("librarians.txt").ToList();

            librarians.Clear();

            for (int i = 0; i < tmp.Count; i++)
            {
                string[] buf = tmp[i].Split(' ');
                librarians.Add(new Librarian(readers, books, buf[0], buf[1], buf[2], buf[3], buf[4]));
            }
        }

        public void GetAdmins()
        {
            List<string> tmp = File.ReadAllLines("admins.txt").ToList();

            admins.Clear();

            for (int i = 0; i < tmp.Count; i++)
            {
                string[] buf = tmp[i].Split(' ');
                admins.Add(new Admin(readers, books, librarians, admins, buf[0], buf[1], buf[2], buf[3], buf[4]));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "reader")
            {
                foreach (var item in readers)
                {
                    if (login.Text == item.Login && password.Text == item.Password)
                    {
                        //LibrarianForm newForm = new LibrarianForm(item);
                        //newForm.Show();

                    }
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "librarian")
            {
                foreach (var item in librarians)
                {
                    if (login.Text == item.Login && password.Text == item.Password)
                    {
                        LibrarianForm newForm = new LibrarianForm(item);
                        newForm.Show();
                    }
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "admin")
            {
                foreach (var item in admins)
                {
                    if (login.Text == item.Login && password.Text == item.Password)
                    {
                        AdminForm newForm = new AdminForm(item);
                        newForm.Show();
                    }
                }
            }
        }
    }
}
