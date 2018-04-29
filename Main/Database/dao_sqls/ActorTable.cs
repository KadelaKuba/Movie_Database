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
    public class ActorTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Actor\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Actor\" WHERE ID=@ID";
        public static String SQL_INSERT = "INSERT INTO \"Actor\" VALUES (@ID, @firstName, @lastName, @nationality, @birthplace, @height)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Actor\" WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE \"Actor\" SET firstName = @firstName, lastName = @lastName, nationality = @nationality, " +
            " birthplace = @birthplace, birthplace = @birthplace, height = @height WHERE ID = @ID";

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
        /// <param name="id">user id</param>
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

        /// Delete the record.
        /// <param name="idUser">user id</param>
        /// <returns></returns>
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
                    actor.Height = reader.GetInt32(i);
                }

                actors.Add(actor);
            }
            return actors;
        }
    }
}
