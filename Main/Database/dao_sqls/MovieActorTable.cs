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
    public class MovieActorTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"MovieActor\"";
        public static String SQL_SELECT_MOVIE_ID = "SELECT * FROM \"MovieActor\" WHERE Movie_ID=@Movie_ID";
        public static String SQL_SELECT_ACTOR_ID = "SELECT * FROM \"MovieActor\" WHERE Actor_ID=@Actor_ID";
        public static String SQL_INSERT = "INSERT INTO \"MovieActor\" VALUES (@Movie_ID, @Actor_ID, @role, @fee)";
        public static String SQL_DELETE_ID = "DELETE FROM \"MovieActor\" WHERE Movie_ID=@Movie_ID and Actor_ID=@Actor_ID";
        public static String SQL_UPDATE = "UPDATE \"MovieActor\" SET role = @role, " +
            " fee = @fee WHERE Movie_ID=@Movie_ID and Actor_ID=@Actor_ID";

        /// Insert the record.
        public static int Insert(MovieActor movieActor, Database pDb = null)
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
            PrepareCommand(command, movieActor);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// Update the record.
        public static int Update(MovieActor movieActor, Database pDb = null)
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
            PrepareCommand(command, movieActor);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// Select the records.
        public static Collection<MovieActor> Select(Database pDb = null)
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

            Collection<MovieActor> movieActor = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return movieActor;
        }

        /// Select the record.
        /// <param name="id">user id</param>
        public static MovieActor SelectMovieId(int Movie_ID, Database pDb = null)
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

            Collection<MovieActor> movieActors = Read(reader);
            MovieActor movieActor = null;
            if (movieActors.Count == 1)
            {
                movieActor = movieActors[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return movieActor;
        }

        /// Select the record.
        /// <param name="id">user id</param>
        public static MovieActor SelectActorID(int Actor_ID, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_ACTOR_ID);

            command.Parameters.AddWithValue("@Actor_ID", Actor_ID);
            SqlDataReader reader = db.Select(command);

            Collection<MovieActor> movieActors = Read(reader);
            MovieActor movieActor = null;
            if (movieActors.Count == 1)
            {
                movieActor = movieActors[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return movieActor;
        }

        /// Delete the record.
        /// <param name="idUser">user id</param>
        /// <returns></returns>
        public static int Delete(int Actor_ID, int Movie_ID, Database pDb = null)
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

            command.Parameters.AddWithValue("@Actor_ID", Actor_ID);
            command.Parameters.AddWithValue("@Movie_ID", Movie_ID);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        ///  Prepare a command.
        private static void PrepareCommand(SqlCommand command, MovieActor movieActor)
        {
            command.Parameters.AddWithValue("@Movie_ID", movieActor.Movie_id);
            command.Parameters.AddWithValue("@Actor_ID", movieActor.Actor_id);
            command.Parameters.AddWithValue("@role", movieActor.Role);
            command.Parameters.AddWithValue("@fee", movieActor.Fee);
        }

        private static Collection<MovieActor> Read(SqlDataReader reader)
        {
            Collection<MovieActor> movieActors = new Collection<MovieActor>();

            while (reader.Read())
            {
                int i = -1;
                MovieActor movieActor = new MovieActor();
                movieActor.Movie_id = reader.GetInt32(++i);
                movieActor.Actor_id = reader.GetInt32(++i);
                movieActor.Role = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    movieActor.Fee = reader.GetInt32(i);
                }

                movieActors.Add(movieActor);
            }
            return movieActors;
        }
    }
}
