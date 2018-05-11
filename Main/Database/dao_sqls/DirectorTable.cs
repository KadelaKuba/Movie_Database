using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Main.ORM.DAO.Sqls
{
    public class DirectorTable
    {
        public static String SQL_SELECT = "SELECT * FROM Director";
        public static String SQL_SELECT_ID = "SELECT * FROM Director WHERE ID=@ID";
        public static String SQL_SELECT_DIRECTOR_FOR_MOVIE = "SELECT D.ID, D.firstName, D.lastName, D.nationality, D.birthplace, D.height " +
            "FROM Director D JOIN Movie M ON M.Director_ID = D.ID WHERE M.ID = @ID";
        public static String SQL_INSERT = "INSERT INTO Director VALUES (@ID, @firstName, @lastName, @nationality, @birthplace, @height)";
        public static String SQL_DELETE_ID = "DELETE FROM Director WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE Director SET firstName = @firstName, lastName = @lastName, nationality = @nationality, " +
            " birthplace = @birthplace, height = @height WHERE ID = @ID";

        public static int Insert(Director director, Database pDb = null)
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
            PrepareCommand(command, director);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Update(Director director, Database pDb = null)
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
            PrepareCommand(command, director);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        public static Collection<Director> Select(Database pDb = null)
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

            Collection<Director> directors = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return directors;
        }

        public static Director Select(int ID, Database pDb = null)
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

            Collection<Director> directors = Read(reader);
            Director director = null;
            if (directors.Count == 1)
            {
                director = directors[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return director;
        }

        public static Collection<Director> SelectDirectorForMovie(int Movie_ID, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_DIRECTOR_FOR_MOVIE);
            command.Parameters.AddWithValue("@ID", Movie_ID);

            SqlDataReader reader = db.Select(command);

            Collection<Director> directors = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return directors;
        }

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

        private static void PrepareCommand(SqlCommand command, Director director)
        {
            command.Parameters.AddWithValue("@ID", director.Id);
            command.Parameters.AddWithValue("@firstName", director.Firstname);
            command.Parameters.AddWithValue("@lastName", director.Lastname);
            command.Parameters.AddWithValue("@nationality", director.Nationality);
            command.Parameters.AddWithValue("@birthplace", director.Birthplace);
            command.Parameters.AddWithValue("@height", director.Height);
        }

        private static Collection<Director> Read(SqlDataReader reader)
        {
            Collection<Director> directors = new Collection<Director>();

            while (reader.Read())
            {
                int i = -1;
                Director director = new Director();
                director.Id = reader.GetInt32(++i);
                director.Firstname = reader.GetString(++i);
                director.Lastname = reader.GetString(++i);
                director.Nationality = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    director.Birthplace = reader.GetString(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    director.Height = reader.GetDecimal(i);
                }

                directors.Add(director);
            }
            return directors;
        }
    }
}
