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
        public double Rate { get; set;} //rename the attribute
        public DateTime Dateofadd { get; set; }
        public string Comment { get; set; }
}
}
