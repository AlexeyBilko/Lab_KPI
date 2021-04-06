using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI_Lab
{
    public class Book
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string DateOfIsuue { get; set; }

        public Book(string title, int id, string date)
        {
            Title = title;
            Id = id;
            DateOfIsuue = date;
        }

        public Book()
        {

        }

        public string GetString()
        {
            return Title + " " + Id.ToString() + " " + DateOfIsuue;
        }
    }
}
