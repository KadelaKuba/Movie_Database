using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ORM
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public string Rank { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string Shortinfo { get; set; }

        public override string ToString()
        {
            return "ID: " + Id + " Jméno a příjmeni:  " + Firstname + " " + Lastname + " Email: " + Email + " points " + Points;
        }
    }
}
