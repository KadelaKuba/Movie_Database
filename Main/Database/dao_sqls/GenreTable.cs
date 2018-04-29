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
    public class GenreTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Genre\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Genre\" WHERE ID=@ID";
        public static String SQL_INSERT = "INSERT INTO \"Genre\" VALUES (@ID, @name, @description)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Genre\" WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE \"Genre\" SET name = @name, description = @description";

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
        /// <param name="id">user id</param>
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
