using System;

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
            return "Movie_id: " + Movie_id + ", User_id:" + User_id + ", Rate: " + Rate + " , Comment: " + Comment;
        }
    }
}
