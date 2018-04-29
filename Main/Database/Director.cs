using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ORM
{
    public class Director
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nationality { get; set; }
        public string Birthplace { get; set; }
        public decimal? Height { get; set;}

        public override string ToString()
        {
            return "ID: " + Id + ", FirstName: " + Firstname + ", LastName: " + Lastname;
        }
    }
}
