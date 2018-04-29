using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ORM
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Time { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public bool? Award { get; set; }
        public DateTime Premiere { get; set; }
        public int Director_id { get; set; }

        public override string ToString()
        {
            return "ID: " + Id;
        }
    }
}
