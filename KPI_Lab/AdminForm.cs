using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace KPI_Lab
{
    public partial class AdminForm : Form
    {
        Admin admin;
        Reader SelectedReader;
        public List<Book> books;
        public List<Reader> readers;
        public List<Librarian> librarians;
        public List<Admin> admins;

        public AdminForm()
        {
            InitializeComponent();

        }

        public AdminForm(Admin a)
        {
            InitializeComponent();
            admin = a;
            SelectedReader = admin.readers[0];
            foreach (var item in a.readers)
            {
                listBox1.Items.Add(item.Name + " " + item.Surname);
            }
            groupBox1.Text = "";
            label8.Text = a.Name;
            label9.Text = a.Surname;
            label10.Text = a.Login;
            /*
            GetBooks();
            GetReaders();
            GetLibrarians();
            GetAdmins();
            */
            status.Items.Add("admin");
            status.Items.Add("librarian");
            status.Items.Add("reader");
            admin.ReadAdmins("admins.txt");
            if (admin.admins.Count != 0)
            {
                foreach (var item in admins)
                {
                    listBox3.Items.Add(item);
                }
            }
        }
        /*
        public void GetBooks()
        {
            try
            {
                List<string> tmp = File.ReadAllLines("books.txt").ToList();

                for (int i = 0; i < tmp.Count; i++)
                {
                    string[] buf = tmp[i].Split(' ');
                    books.Add(new Book(buf[0], Convert.ToInt32(buf[1]), buf[2]));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
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
        }*/

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() != null)
            {
                SelectedReader = admin.readers.Find(x => x.Name + " " + x.Surname == listBox1.SelectedItem.ToString());
            }
            listBox2.Items.Clear();

            foreach (var item in SelectedReader.books)
            {
                listBox2.Items.Add(item.Title);
            }

            Name.Text = SelectedReader.Name;
            Surname.Text = SelectedReader.Surname;
            DateOfBirth.Text = SelectedReader.DateOfBirth;
            login.Text = SelectedReader.Login;
            fine.Text = SelectedReader.fine.ToString();
            password.Text = "********";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Name.Text = "";
            Surname.Text = "";
            DateOfBirth.Text = "";
            login.Text = "";
            fine.Text = "";
            password.Text = "";
            fine.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Reader reader = new Reader(Name.Text, Surname.Text, DateOfBirth.Text, login.Text, password.Text);
                Admin a = new Admin(readers, books, librarians, admins, Name.Text, Surname.Text, DateOfBirth.Text, login.Text, password.Text);
                Librarian l = new Librarian(readers, books, Name.Text, Surname.Text, DateOfBirth.Text, login.Text, password.Text);
                fine.Enabled = true;

                Name.Text = "";
                Surname.Text = "";
                DateOfBirth.Text = "";
                login.Text = "";
                fine.Text = "";
                password.Text = "";
                if (status.SelectedItem.ToString() == "reader")
                {
                    listBox1.Items.Add(reader.Name + " " + reader.Surname);

                    admin.readers.Add(reader);
                    admin.SaveReadersChangesInFile("readers.txt");
                }
                else if (status.SelectedItem.ToString() == "admin")
                {
                    listBox3.Items.Add(a.Name + " " + a.Surname);

                    admin.AddAdmin(a);
                }
                else if (status.SelectedItem.ToString() == "librarian")
                {
                    listBox3.Items.Add(l.Name + " " + l.Surname);

                    admin.AddLibrarian(l);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Reader reader = admin.readers.Find(x => x.Name + " " + x.Surname == listBox1.SelectedItem.ToString());
                admin.readers.Remove(reader);
                admin.SaveReadersChangesInFile("readers.txt");

                listBox1.Items.Clear();

                foreach (var item in admin.readers)
                {
                    listBox1.Items.Add(item.Name + " " + item.Surname);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = admin.books.Find(x => x.Title == textBox1.Text);
                Reader reader = admin.readers.Find(x => x.Name + " " + x.Surname == listBox1.SelectedItem.ToString());
                if (reader.books.Find(x => x == book) != null)
                {
                    MessageBox.Show("This book is already here", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    reader.books.Add(book);
                    admin.SaveReadersChangesInFile("readers.txt");

                    listBox2.Items.Clear();

                    foreach (var item in reader.books)
                    {
                        listBox2.Items.Add(item.Title);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = admin.books.Find(x => x.Title == listBox2.SelectedItem.ToString());
                Reader reader = admin.readers.Find(x => x.Name + " " + x.Surname == listBox1.SelectedItem.ToString());
                reader.books.Remove(book);
                admin.SaveReadersChangesInFile("readers.txt");

                listBox2.Items.Clear();

                foreach (var item in reader.books)
                {
                    listBox2.Items.Add(item.Title);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            if (status.SelectedItem.ToString() == "librarian")
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
            else if (status.SelectedItem.ToString() == "admin")
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
            else if (status.SelectedItem.ToString() == "reader")
            {
                foreach (var item in readers)
                {
                    if (login.Text == item.Login && password.Text == item.Password)
                    {
                        ReaderForm newForm = new ReaderForm(item);
                        newForm.Show();
                    }
                }
            }
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}