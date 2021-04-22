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
        Admin SelectedAdmin;
        Librarian SelectedLibrarian;

        public AdminForm()
        {
            InitializeComponent();
        }

        public AdminForm(Admin a)
        {
            InitializeComponent();

            admin = a;
            SelectedLibrarian = admin.librarians[0];
            SelectedAdmin = admin.admins[0];
            SelectedReader = admin.readers[0];

            foreach (var item in admin.librarians)
            {
                listBox4.Items.Add(item.Name + " " + item.Surname);
            }

            if (admin.admins.Count != 0)
            {
                foreach (var item in admin.admins)
                {
                    listBox3.Items.Add(item.Name + " " + item.Surname);
                }
            }

            foreach (var item in admin.readers)
            {
                listBox1.Items.Add(item.Name + " " + item.Surname);
            }

            groupBox1.Text = "";

            label8.Text = a.Name;
            label9.Text = a.Surname;
            label10.Text = a.Login;

            status.Items.Add("admin");
            status.Items.Add("librarian");
            status.Items.Add("reader");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    textBox1.Visible = true;
                    button1.Visible = true;
                    button2.Visible = true;

                    SelectedReader = admin.readers.Find(x => x.Name + " " + x.Surname == listBox1.SelectedItem.ToString());

                    listBox2.Items.Clear();

                    foreach (var item in SelectedReader.books)
                    {
                        listBox2.Items.Add(item.Title);
                    }

                    _Name.Text = SelectedReader.Name;
                    Surname.Text = SelectedReader.Surname;
                    DateOfBirth.Text = SelectedReader.DateOfBirth;
                    login.Text = SelectedReader.Login;
                    fine.Text = SelectedReader.fine.ToString();
                    password.Text = "********";

                    status.SelectedItem = "reader";

                    listBox3.ClearSelected();
                    listBox4.ClearSelected();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
            try
            {
                if (listBox3.SelectedItem != null)
                {
                    textBox1.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;

                    SelectedAdmin = admin.admins.Find(x => x.Name + " " + x.Surname == listBox3.SelectedItem.ToString());

                    _Name.Text = SelectedAdmin.Name;
                    Surname.Text = SelectedAdmin.Surname;
                    DateOfBirth.Text = SelectedAdmin.DateOfBirth;
                    login.Text = SelectedAdmin.Login;
                    fine.Text = "";
                    password.Text = "********";


                    status.SelectedItem = "admin";


                    listBox2.Items.Clear();
                    listBox4.ClearSelected();
                    listBox1.ClearSelected();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
            try
            {
                if (listBox4.SelectedItem != null)
                {
                    textBox1.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;

                    SelectedLibrarian = admin.librarians.Find(x => x.Name + " " + x.Surname == listBox4.SelectedItem.ToString());

                    _Name.Text = SelectedLibrarian.Name;
                    Surname.Text = SelectedLibrarian.Surname;
                    DateOfBirth.Text = SelectedLibrarian.DateOfBirth;
                    login.Text = SelectedLibrarian.Login;
                    fine.Text = "";
                    password.Text = "********";

                    status.SelectedItem = "librarian";

                    listBox2.Items.Clear();
                    listBox3.ClearSelected();
                    listBox1.ClearSelected();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _Name.Text = "";
            Surname.Text = "";
            DateOfBirth.Text = "";
            login.Text = "";
            fine.Text = "";
            password.Text = "";
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Name.Text == "" || Surname.Text == "" || DateOfBirth.Text == "" || login.Text == "" || password.Text == "")
                {
                    MessageBox.Show("Some field is empty");
                }
                else
                {
                    if (status.SelectedItem.ToString() == "reader")
                    {
                        Reader reader = new Reader(_Name.Text, Surname.Text, DateOfBirth.Text, login.Text, password.Text);

                        _Name.Text = "";
                        Surname.Text = "";
                        DateOfBirth.Text = "";
                        login.Text = "";
                        fine.Text = "";
                        password.Text = "";

                        listBox1.Items.Add(reader.Name + " " + reader.Surname);

                        admin.readers.Add(reader);
                        admin.SaveReadersChangesInFile("readers.txt");
                    }
                    else if (status.SelectedItem.ToString() == "librarian")
                    {
                        Librarian librarian = new Librarian(admin.readers, admin.books, _Name.Text, Surname.Text, DateOfBirth.Text, login.Text, password.Text);

                        _Name.Text = "";
                        Surname.Text = "";
                        DateOfBirth.Text = "";
                        login.Text = "";
                        fine.Text = "";
                        password.Text = "";

                        listBox4.Items.Add(librarian.Name + " " + librarian.Surname);

                        admin.librarians.Add(librarian);
                        admin.SaveLibrariansChangesInFile("librarians.txt");
                    }
                    else if (status.SelectedItem.ToString() == "admin")
                    {
                        Admin a = new Admin(admin.readers, admin.books, admin.librarians, admin.admins, _Name.Text, Surname.Text, DateOfBirth.Text, login.Text, password.Text);

                        _Name.Text = "";
                        Surname.Text = "";
                        DateOfBirth.Text = "";
                        login.Text = "";
                        fine.Text = "";
                        password.Text = "";

                        listBox3.Items.Add(a.Name + " " + a.Surname);

                        admin.admins.Add(a);
                        admin.SaveAdminsChangesInFile("admins.txt");
                    }
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
                if (status.SelectedItem.ToString() == "reader")
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
                else if(status.SelectedItem.ToString() == "librarian")
                {
                    Librarian reader = admin.librarians.Find(x => x.Name + " " + x.Surname == listBox4.SelectedItem.ToString());

                    admin.librarians.Remove(reader);
                    admin.SaveLibrariansChangesInFile("librarian.txt");

                    listBox4.Items.Clear();

                    foreach (var item in admin.librarians)
                    {
                        listBox4.Items.Add(item.Name + " " + item.Surname);
                    }
                }
                else if(status.SelectedItem.ToString() == "admin")
                {
                    Admin a = admin.admins.Find(x => x.Name + " " + x.Surname == listBox3.SelectedItem.ToString());
                    // фикс бага
                    if (a.Name != label8.Text && a.Name != label9.Text)
                    {
                        admin.admins.Remove(a);
                        admin.SaveAdminsChangesInFile("admins.txt");

                        listBox3.Items.Clear();

                        if (admin.admins.Count != 0)
                        {
                            foreach (var item in admin.admins)
                            {
                                listBox3.Items.Add(item.Name + " " + item.Surname);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot delete yourself!");
                    }
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
    }
}