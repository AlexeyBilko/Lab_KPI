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
    public partial class ReaderForm : Form
    {
        Reader reader;
        public ReaderForm()
        {
            InitializeComponent();
        }
        public ReaderForm(Reader r)
        {
            InitializeComponent();
            reader = r;
            label1.Text = r.Name;
            label2.Text = r.Surname;
            label3.Text = r.Login;
            label4.Text = r.fine.ToString();
            label8.Text = r.DateOfBirth;
            if (r.books.Count != 0)
            {
                foreach (var item in r.books)
                {
                    listBox1.Items.Add(item.Id + "\t" + item.Title + "\t" + item.DateOfIsuue);
                }
            }
        }
    }
}
