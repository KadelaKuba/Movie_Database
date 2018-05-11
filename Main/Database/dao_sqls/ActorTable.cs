using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Main.ORM.DAO.Sqls
{
    public class ActorTable
    {
        public static String SQL_SELECT = "SELECT * FROM Actor";
        public static String SQL_SELECT_ID = "SELECT * FROM Actor WHERE ID=@ID";
        public static String SQL_SELECT_ACTORS_FOR_MOVIE = "SELECT A.ID, A.firstName, A.lastName, A.nationality, A.birthplace, A.height " +
            "FROM Movie M JOIN MovieActor MA ON MA.Movie_ID = M.ID JOIN Actor A ON A.ID = MA.Actor_ID " +
            "WHERE MA.Movie_ID = @Movie_ID";
        public static String SQL_INSERT = "INSERT INTO Actor VALUES (@ID, @firstName, @lastName, @nationality, @birthplace, @height)";
        public static String SQL_DELETE_ID = "DELETE FROM Actor WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE Actor SET firstName = @firstName, lastName = @lastName, nationality = @nationality, " +
            "birthplace = @birthplace, height = @height WHERE ID = @ID";

        /// Insert the record.
        public static int Insert(Actor actor, Database pDb = null)
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
            PrepareCommand(command, actor);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// Update the record.
        public static int Update(Actor actor, Database pDb = null)
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
            PrepareCommand(command, actor);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// Select the records.
        public static Collection<Actor> Select(Database pDb = null)
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

            Collection<Actor> actors = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return actors;
        }

        /// Select the record.
        public static Actor Select(int ID, Database pDb = null)
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

            Collection<Actor> actors = Read(reader);
            Actor actor = null;
            if (actors.Count == 1)
            {
                actor = actors[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return actor;
        }

        /// Select the records.
        public static Collection<Actor> SelectActorsForMovie(int Movie_ID, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_ACTORS_FOR_MOVIE);
            command.Parameters.AddWithValue("@Movie_ID", Movie_ID);
            //Console.WriteLine(command.CommandText);
            SqlDataReader reader = db.Select(command);

            Collection<Actor> actors = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return actors;
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
        private static void PrepareCommand(SqlCommand command, Actor actor)
        {
            command.Parameters.AddWithValue("@ID", actor.Id);
            command.Parameters.AddWithValue("@firstName", actor.Firstname);
            command.Parameters.AddWithValue("@lastName", actor.Lastname);
            command.Parameters.AddWithValue("@nationality", actor.Nationality);
            command.Parameters.AddWithValue("@birthplace", actor.Birthplace);
            command.Parameters.AddWithValue("@height", actor.Height);
        }

        private static Collection<Actor> Read(SqlDataReader reader)
        {
            Collection<Actor> actors = new Collection<Actor>();

            while (reader.Read())
            {
                int i = -1;
                Actor actor = new Actor();
                actor.Id = reader.GetInt32(++i);
                actor.Firstname = reader.GetString(++i);
                actor.Lastname = reader.GetString(++i);
                actor.Nationality = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    actor.Birthplace = reader.GetString(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    actor.Height = reader.GetDecimal(i);
                }

                actors.Add(actor);
            }
            return actors;
        }
    }
}
