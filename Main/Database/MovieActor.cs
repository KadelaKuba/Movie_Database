using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ORM
{
    public class MovieActor
    {
        public int Movie_id { get; set; }
        public int Actor_id { get; set; }
        public string Role { get; set; }
        public int? Fee { get; set; }

        public override string ToString()
        {
            return "Movie_id: " + Movie_id + ", Actor_id: " + Actor_id + ", Role: " + Role + ", Fee:" + Fee;
        }
    }
}
