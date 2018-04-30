using System;

namespace Main.ORM
{
    public class MovieGenre
    {
        public int Movie_id { get; set; }
        public int Genre_id { get; set; }
        public DateTime? dateOfAdd { get; set; }

        public override string ToString()
        {
            return "Movie_id: " + Movie_id + ", Genre_id: " + Genre_id + ", Date of add: " + dateOfAdd;
        }
    }
}
