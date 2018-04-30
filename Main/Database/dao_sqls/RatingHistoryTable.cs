using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Main.ORM.DAO.Sqls
{
    class RatingHistoryTable
    {
        public static String SQL_SELECT = "SELECT * FROM RatingHistory";
        public static String SQL_SELECT_ID = "SELECT * FROM RatingHistory WHERE ID=@ID";
        public static String SQL_INSERT = "INSERT INTO RatingHistory VALUES (@ID, @rate, @dateOfChange, @comment, @Rating_movie_id, @Rating_userinfo_id)";
        public static String SQL_DELETE_ID = "DELETE FROM RatingHistory WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE RatingHistory SET rate = @rate, " +
            " dateOfChange = @dateOfChange, comment = @comment, Rating_movie_id = @Rating_movie_id, Rating_userinfo_id = @Rating_userinfo_id WHERE ID=@ID";

        /// Insert the record.
        public static int Insert(RatingHistory ratingHistory, Database pDb = null)
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
            PrepareCommand(command, ratingHistory);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// Update the record.
        public static int Update(RatingHistory ratingHistory, Database pDb = null)
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
            PrepareCommand(command, ratingHistory);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// Select the records.
        public static Collection<RatingHistory> Select(Database pDb = null)
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

            Collection<RatingHistory> ratingHistories = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return ratingHistories;
        }

        /// Select the record.
        public static RatingHistory Select(int ID, Database pDb = null)
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

            Collection<RatingHistory> ratingHistories = Read(reader);
            RatingHistory ratingHistory = null;
            if (ratingHistories.Count == 1)
            {
                ratingHistory = ratingHistories[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return ratingHistory;
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
        private static void PrepareCommand(SqlCommand command, RatingHistory ratingHistory)
        {
            command.Parameters.AddWithValue("@ID", ratingHistory.Id);
            command.Parameters.AddWithValue("@rate", ratingHistory.Rate);
            command.Parameters.AddWithValue("@dateOfChange", ratingHistory.Dateofchange);
            command.Parameters.AddWithValue("@comment", ratingHistory.Comment);
            command.Parameters.AddWithValue("@Rating_movie_id", ratingHistory.Rating_movie_id);
            command.Parameters.AddWithValue("@Rating_userinfo_id", ratingHistory.Rating_userinfo_id);
        }

        private static Collection<RatingHistory> Read(SqlDataReader reader)
        {
            Collection<RatingHistory> ratingHistories = new Collection<RatingHistory>();

            while (reader.Read())
            {
                int i = -1;
                RatingHistory ratingHistory = new RatingHistory();
                ratingHistory.Id = reader.GetInt32(++i);
                ratingHistory.Rate = reader.GetDecimal(++i);
                ratingHistory.Dateofchange = reader.GetDateTime(++i);
                if (!reader.IsDBNull(++i))
                {
                    ratingHistory.Comment = reader.GetString(i);
                }
                ratingHistory.Rating_movie_id = reader.GetInt32(++i);
                ratingHistory.Rating_userinfo_id = reader.GetInt32(++i);

                ratingHistories.Add(ratingHistory);
            }
            return ratingHistories;
        }
    }
}
