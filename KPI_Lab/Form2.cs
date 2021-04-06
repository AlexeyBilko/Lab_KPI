using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI_Lab
{
    public partial class Form2 : Form
    {
        public List<Book> books;
        public List<Reader> readers;
        public List<Librarian> librarians;
        public List<Admin> admins;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(List<Book> b, List<Reader> r, List<Librarian> l)
        {
            books = b;
            readers = r;
            librarians = l;
            InitializeComponent();


            foreach (var item in books)
            {
                listBox1.Items.Add(item.GetString());
            }
        }
        public Form2(List<Book> b, List<Reader> r, List<Librarian> l, List<Admin> a)
        {
            books = b;
            readers = r;
            librarians = l;
            admins = a;
            InitializeComponent();


            foreach (var item in books)
            {
                listBox1.Items.Add(item.GetString());
            }
        }
    }
}
