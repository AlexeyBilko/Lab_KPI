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
    public partial class AdminForm : Form
    {
        Admin admin;
        Reader SelectedReader;

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
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedReader = admin.readers.Find(x => x.Name + " " + x.Surname == listBox1.SelectedItem.ToString());

            textBox2.Text = SelectedReader.Name;
            textBox3.Text = SelectedReader.Surname;
            textBox4.Text = SelectedReader.DateOfBirth;
            textBox5.Text = SelectedReader.Login;
            textBox6.Text = "********";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Reader reader = new Reader(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                textBox6.Text = "";

                listBox1.Items.Add(reader.Name + " " + reader.Surname);

                admin.readers.Add(reader);
                admin.SaveReadersChangesInFile("readers.txt");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
