using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Main.ORM.DAO.Sqls
{
    class MovieGenreTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"MovieGenre\"";
        public static String SQL_SELECT_MOVIE_ID = "SELECT * FROM \"MovieGenre\" WHERE Movie_ID=@Movie_ID";
        public static String SQL_SELECT_GENRE_ID = "SELECT * FROM \"MovieGenre\" WHERE Genre_ID=@Genre_ID";
        public static String SQL_INSERT = "INSERT INTO \"MovieGenre\" VALUES (@Movie_ID, @Genre_ID, @dateOfAdd)";
        public static String SQL_DELETE_ID = "DELETE FROM \"MovieGenre\" WHERE Movie_ID=@Movie_ID and Genre_ID=@Genre_ID";
        public static String SQL_UPDATE = "UPDATE \"MovieGenre\" SET dateOfAdd = @dateOfAdd WHERE Movie_ID = @Movie_ID and Genre_ID = @Genre_ID";

        /// Insert the record.
        public static int Insert(MovieGenre movieGenre, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, movieGenre);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// Update the record.
        public static int Update(MovieGenre movieGenre, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, movieGenre);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// Select the records.
        public static Collection<MovieGenre> Select(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<MovieGenre> movieGenres = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return movieGenres;
        }

        /// Select the record.
        /// <param name="id">user id</param>
        public static MovieGenre SelectMovieId(int Movie_ID, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_MOVIE_ID);

            command.Parameters.AddWithValue("@Movie_ID", Movie_ID);
            SqlDataReader reader = db.Select(command);

            Collection<MovieGenre> movieGenres = Read(reader);
            MovieGenre movieGenre = null;
            if (movieGenres.Count == 1)
            {
                movieGenre = movieGenres[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return movieGenre;
        }

        /// Select the record.
        /// <param name="id">user id</param>
        public static MovieGenre SelectGenreId(int Genre_ID, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_GENRE_ID);

            command.Parameters.AddWithValue("@Genre_ID", Genre_ID);
            SqlDataReader reader = db.Select(command);

            Collection<MovieGenre> movieGenres = Read(reader);
            MovieGenre movieGenre = null;
            if (movieGenres.Count == 1)
            {
                movieGenre = movieGenres[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return movieGenre;
        }

        /// Delete the record.
        /// <param name="idUser">user id</param>
        /// <returns></returns>
        public static int Delete(int Movie_ID, int Genre_ID, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@Movie_ID", Movie_ID);
            command.Parameters.AddWithValue("@Genre_ID", Genre_ID);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        ///  Prepare a command.
        private static void PrepareCommand(SqlCommand command, MovieGenre movieGenre)
        {
            command.Parameters.AddWithValue("@Movie_ID", movieGenre.Movie_id);
            command.Parameters.AddWithValue("@Genre_ID", movieGenre.Genre_id);
            command.Parameters.AddWithValue("@dateOfAdd", movieGenre.dateOfAdd);
        }

        private static Collection<MovieGenre> Read(SqlDataReader reader)
        {
            Collection<MovieGenre> movieGenres = new Collection<MovieGenre>();

            while (reader.Read())
            {
                int i = -1;
                MovieGenre movieGenre = new MovieGenre();
                movieGenre.Movie_id = reader.GetInt32(++i);
                movieGenre.Genre_id = reader.GetInt32(++i);
                if (!reader.IsDBNull(++i))
                {
                    movieGenre.dateOfAdd = reader.GetDateTime(i);
                }

                movieGenres.Add(movieGenre);
            }
            return movieGenres;
        }
    }
}
