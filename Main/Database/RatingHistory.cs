using System;

namespace Main.ORM
{
    public class RatingHistory
    {
        public int Id { get; set; }
        public decimal Rate { get; set;}
        public DateTime Dateofchange { get; set; }
        public string Comment { get; set; }
        public int Rating_movie_id { get; set; }
        public int Rating_userinfo_id { get; set; }
        public override string ToString()
        {
            return "Movie_id: " + Rating_movie_id + ", User_id:" + Rating_userinfo_id + ", Rate: " + Rate + " , Comment: " + Comment;
        }
    }
}
