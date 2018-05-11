using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Main.ORM.DAO.Sqls
{
    public class GenreTable
    {
        public static String SQL_SELECT = "SELECT * FROM Genre";
        public static String SQL_SELECT_ID = "SELECT * FROM Genre WHERE ID=@ID";
        public static String SQL_SELECT_GENRES_FOR_MOVIE = "SELECT G.ID, G.name, G.description FROM Movie M " +
            "JOIN MovieGenre MG ON MG.Movie_ID = M.ID JOIN Genre G ON G.ID = MG.Genre_ID " +
            "WHERE MG.Movie_ID = @Movie_ID";
        public static String SQL_INSERT = "INSERT INTO Genre VALUES (@ID, @name, @description)";
        public static String SQL_DELETE_ID = "DELETE FROM Genre WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE Genre SET name = @name, description = @description";

        /// Insert the record.
        public static int Insert(Genre genre, Database pDb = null)
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
            PrepareCommand(command, genre);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// Update the record.
        public static int Update(Genre genre, Database pDb = null)
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
            PrepareCommand(command, genre);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// Select the records.
        public static Collection<Genre> Select(Database pDb = null)
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

            Collection<Genre> genres = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return genres;
        }

        /// Select the record.
        public static Genre Select(int ID, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@ID", ID);
            SqlDataReader reader = db.Select(command);

            Collection<Genre> genres = Read(reader);
            Genre genre = null;
            if (genres.Count == 1)
            {
                genre = genres[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return genre;
        }

        /// Select the records.
        public static Collection<Genre> SelectGenresForMovie(int Movie_ID, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_GENRES_FOR_MOVIE);
            command.Parameters.AddWithValue("@Movie_ID", Movie_ID);

            SqlDataReader reader = db.Select(command);

            Collection<Genre> genres = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return genres;
        }

        /// Delete the record.
        public static int Delete(int ID, Database pDb = null)
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

            command.Parameters.AddWithValue("@ID", ID);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        ///  Prepare a command.
        private static void PrepareCommand(SqlCommand command, Genre genre)
        {
            command.Parameters.AddWithValue("@ID", genre.Id);
            command.Parameters.AddWithValue("@name", genre.Name);
            command.Parameters.AddWithValue("@description", genre.Description);
        }

        private static Collection<Genre> Read(SqlDataReader reader)
        {
            Collection<Genre> genres = new Collection<Genre>();

            while (reader.Read())
            {
                int i = -1;
                Genre genre = new Genre();
                genre.Id = reader.GetInt32(++i);
                genre.Name = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    genre.Description = reader.GetString(i);
                }

                genres.Add(genre);
            }
            return genres;
        }
    }
}
