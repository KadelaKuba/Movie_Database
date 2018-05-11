using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Main.ORM.DAO.Sqls
{
    public class MovieTable
    {
        public static String SQL_SELECT = "SELECT M.ID, M.title, M.year, M.time, M.language, M.description, M.country, M.award, M.premiere, " +
            "M.Director_ID, AVG(R.rate) FROM Movie M LEFT JOIN Rating R ON R.Movie_ID = M.ID " +
            "GROUP BY M.ID, M.title, M.year, M.time, M.language, M.description, M.country, M.award, M.premiere, M.Director_ID, M.title";
        public static String SQL_SELECT_ID = "SELECT * FROM Movie WHERE ID=@ID";
        public static String SQL_INSERT = "INSERT INTO Movie VALUES (@ID, @title, @year, @time, @language, " +
            "@description, @country, @award, @premiere, @Director_ID)";
        public static String SQL_DELETE_ID = "DELETE FROM Movie WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE Movie SET ID = @ID, title = @title, year = @year, " +
            " time = @time, language = @language, description = @description, country = @country, award = @award, premiere = @premiere, " +
            " Director_ID = @Director_ID WHERE ID=@ID";
        public static String SQL_ACTOR_NUMBER = "SELECT M.id, Count(MA.Movie_ID) " +
            "FROM Movie M LEFT JOIN MovieActor MA ON M.ID = MA.Movie_ID " +
            "GROUP BY M.id";

        public static int Insert(Movie movie, Database pDb = null)
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
            PrepareCommand(command, movie);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Update(Movie movie, Database pDb = null)
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
            PrepareCommand(command, movie);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        public static Collection<Movie> Select(Database pDb = null)
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

            Collection<Movie> movies = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return movies;
        }

        public static Movie Select(int id, Database pDb = null)
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

            command.Parameters.AddWithValue("@ID", id);
            SqlDataReader reader = db.Select(command);

            Collection<Movie> Movies = Read(reader);
            Movie Movie = null;
            if (Movies.Count == 1)
            {
                Movie = Movies[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Movie;
        }

        public static int Delete(int id, Database pDb = null)
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

            command.Parameters.AddWithValue("@ID", id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static void PrepareCommand(SqlCommand command, Movie Movie)
        {
            command.Parameters.AddWithValue("@ID", Movie.Id);
            command.Parameters.AddWithValue("@title", Movie.Title);
            command.Parameters.AddWithValue("@year", Movie.Year);
            command.Parameters.AddWithValue("@time", Movie.Time);
            command.Parameters.AddWithValue("@language", Movie.Language);
            command.Parameters.AddWithValue("@description", Movie.Description);
            command.Parameters.AddWithValue("@country", Movie.Country);
            command.Parameters.AddWithValue("@award", Movie.Award == null ? DBNull.Value : (object)Movie.Award);
            command.Parameters.AddWithValue("@premiere", Movie.Premiere);
            command.Parameters.AddWithValue("@Director_ID", Movie.Director_id);

        }

        private static Collection<Movie> Read(SqlDataReader reader)
        {
            Collection<Movie> Movies = new Collection<Movie>();

            while (reader.Read())
            {
                int i = -1;
                Movie Movie = new Movie();
                Movie.Id = reader.GetInt32(++i);
                Movie.Title = reader.GetString(++i);
                Movie.Year = reader.GetInt32(++i);
                Movie.Time = reader.GetInt32(++i);
                Movie.Language = reader.GetString(++i);
                Movie.Description = reader.GetString(++i);
                Movie.Country = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    Movie.Award = reader.GetBoolean(i);
                }
                Movie.Premiere = reader.GetDateTime(++i);
                Movie.Director_id = reader.GetInt32(++i);
                if (!reader.IsDBNull(++i))
                {
                    Movie.AvgRating = reader.GetDecimal(i);
                }

                Movies.Add(Movie);
            }
            return Movies;
        }

        public static int Select_Actor_Number()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_ACTOR_NUMBER);
            SqlDataReader reader = db.Select(command);
            int count = 0;
            while (reader.Read())
            {
                int i = -1;

                if (!reader.IsDBNull(++i))
                {
                    count = reader.GetInt32(++i);
                }

            }
            reader.Close();
            db.Close();

            return count;
        }
    }
}
