using System;

namespace Main.ORM
{
    public class RatingHistory
    {
        public int Id { get; set; }
        public double rate { get; set;} //rename rate attribute
        public DateTime Dateofchange { get; set; }
        public string Comment { get; set; }
        public int Rating_movie_id { get; set; }
        public int Rating_userinfo_id { get; set; }
}
}
