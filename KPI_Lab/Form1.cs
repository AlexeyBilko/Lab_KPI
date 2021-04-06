using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI_Lab
{
    public partial class Form1 : Form
    {
        public List<Book> books;
        public List<Reader> readers;
        public List<Librarian> librarians;


        public Form1()
        {
            InitializeComponent();

            books = new List<Book>();
            readers = new List<Reader>();
            librarians = new List<Librarian>();

            GetBooks();
            GetReaders();
            GetLibrarians();

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
                readers[i].fine = Convert.ToInt32(buf[5]);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "reader")
            {
                foreach (var item in readers)
                {
                    if (login.Text == item.Login && password.Text == item.Password)
                    {
                        Form2 newForm = new Form2(books,readers,librarians);
                        newForm.Show();

                    }
                }
            }
            else if(comboBox1.SelectedItem.ToString() == "librarian")
            {
                foreach (var item in librarians)
                {
                    if (login.Text == item.Login && password.Text == item.Password)
                    {
                        Form2 newForm = new Form2(books, readers, librarians);
                        newForm.Show();
                    }
                }
            }
        }
    }
}
