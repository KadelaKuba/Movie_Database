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
    public class RatingTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"Rating\"";
        public static String SQL_SELECT_MOVIE_ID = "SELECT * FROM \"Rating\" WHERE Movie_ID=@id";
        public static String SQL_SELECT_USER_ID = "SELECT * FROM \"Rating\" WHERE User=@id";
        public static String SQL_INSERT = "INSERT INTO \"Rating\" VALUES (@Movie_ID, @User_ID, @rating, @dateOfAdd, @comment)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Rating\" WHERE Movie_ID=@Movie_ID and User_ID=@User_ID";
        public static String SQL_UPDATE = "UPDATE \"Rating\" SET rating = @rating, " +
            " dateOfAdd = @dateOfAdd, comment = @comment WHERE Movie_ID=@Movie_ID and User_ID=@User_ID";

        /// Insert the record.
        public static int Insert(Rating rating, Database pDb = null)
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
            PrepareCommand(command, rating);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// Update the record.
        public static int Update(Rating rating, Database pDb = null)
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
            PrepareCommand(command, rating);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// Select the records.
        public static Collection<Rating> Select(Database pDb = null)
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

            Collection<Rating> ratings = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return ratings;
        }

        /// Select the record.
        /// <param name="id">user id</param>
        public static Rating SelectMovieID(int Movie_ID, Database pDb = null)
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

            Collection<Rating> Ratings = Read(reader);
            Rating Rating = null;
            if (Ratings.Count == 1)
            {
                Rating = Ratings[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Rating;
        }

        /// Select the record.
        /// <param name="id">user id</param>
        public static Rating SelectUserID(int UserID, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_USER_ID);

            command.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = db.Select(command);

            Collection<Rating> Ratings = Read(reader);
            Rating Rating = null;
            if (Ratings.Count == 1)
            {
                Rating = Ratings[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Rating;
        }

        /// Delete the record.
        /// <param name="idUser">user id</param>
        /// <returns></returns>
        public static int Delete(int UserID, int MovieID, Database pDb = null)
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

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@MovieID", MovieID);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        ///  Prepare a command.
        private static void PrepareCommand(SqlCommand command, Rating Rating)
        {
            command.Parameters.AddWithValue("@Movie_ID", Rating.Movie_id);
            command.Parameters.AddWithValue("@nickname", Rating.User_id);
            command.Parameters.AddWithValue("@firstName", Rating.Rate);
            command.Parameters.AddWithValue("@lastName", Rating.Dateofadd);
            command.Parameters.AddWithValue("@email", Rating.Comment);
        }

        private static Collection<Rating> Read(SqlDataReader reader)
        {
            Collection<Rating> ratings = new Collection<Rating>();

            while (reader.Read())
            {
                int i = -1;
                Rating rating = new Rating();
                rating.Movie_id = reader.GetInt32(++i);
                rating.User_id = reader.GetInt32(++i);
                rating.Rate = reader.GetDouble(++i);
                rating.Dateofadd = reader.GetDateTime(++i);
                if (!reader.IsDBNull(++i))
                {
                    rating.Comment = reader.GetString(i);
                }

                ratings.Add(rating);
            }
            return ratings;
        }
    }
}
