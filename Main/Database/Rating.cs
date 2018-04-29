using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ORM
{
    public class Rating
    {
        public int Movie_id { get; set; }
        public int User_id { get; set; }
        public decimal Rate { get; set;}
        public DateTime Dateofadd { get; set; }
        public string Comment { get; set; }

        public override string ToString()
        {
            return "Rate: " + Rate;
        }
    }
}
