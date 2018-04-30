using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Main.ORM.DAO.Sqls
{
    public class UserTable
    {
        public static String SQL_SELECT = "SELECT * FROM UserInfo";
        public static String SQL_SELECT_ID = "SELECT * FROM UserInfo WHERE ID=@ID";
        public static String SQL_INSERT = "INSERT INTO UserInfo VALUES (@ID, @nickname, @firstName, @lastName, @email, " +
            "@points, @rank, @sex, @country, @shortInfo)";
        public static String SQL_DELETE_ID = "DELETE FROM UserInfo WHERE ID=@ID";
        public static String SQL_UPDATE = "UPDATE UserInfo SET nickname = @nickname, firstName = @firstName, " +
            " lastName = @lastName, email = @email, points = @points, rank = @rank, sex = @sex, country = @country, " +
            " shortInfo = @shortInfo WHERE ID=@ID";
        public static String SQL_NUMBER_OF_MOVIE_RATES = "SELECT U.ID, Count(R.User_ID) AS pocet " +
            "FROM UserInfo U LEFT JOIN Rating R ON R.User_ID = U.ID " +
            "GROUP BY U.ID HAVING Count(R.User_ID) >= @numberOfRates";
        public static String SQL_VALUATE_USER = "EXEC spuUserValuation";
        public static String SQL_RECALCULATE_POINTS = "EXEC spRecalculatePoints";

        /// Insert the record.
        public static int Insert(UserInfo user, Database pDb = null)
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
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);    
            
            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// Update the record.
        public static int Update(UserInfo user, Database pDb = null)
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
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// Select the records.
        public static Collection<UserInfo> Select(Database pDb = null)
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

            Collection<UserInfo> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }

        /// Select the record.
        public static UserInfo Select(int id, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<UserInfo> Users = Read(reader);
            UserInfo User = null;
            if (Users.Count == 1)
            {
                User = Users[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User;
        }

        /// Delete the record.
        public static int Delete(int idUser, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", idUser);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        ///  Prepare a command.
        private static void PrepareCommand(SqlCommand command, UserInfo User)
        {
            command.Parameters.AddWithValue("@ID", User.Id);
            command.Parameters.AddWithValue("@nickname", User.Nickname);
            command.Parameters.AddWithValue("@firstName", User.Firstname);
            command.Parameters.AddWithValue("@lastName", User.Lastname);
            command.Parameters.AddWithValue("@email", User.Email);
            command.Parameters.AddWithValue("@points", User.Points);
            command.Parameters.AddWithValue("@rank", User.Rank);
            command.Parameters.AddWithValue("@sex", User.Sex == null ? DBNull.Value : (object)User.Sex);
            command.Parameters.AddWithValue("@country", User.Country == null ? DBNull.Value : (object)User.Country);
            command.Parameters.AddWithValue("@shortInfo", User.Shortinfo == null ? DBNull.Value : (object)User.Shortinfo);

        }

        private static Collection<UserInfo> Read(SqlDataReader reader)
        {
            Collection<UserInfo> users = new Collection<UserInfo>();

            while (reader.Read())
            {
                int i = -1;
                UserInfo user = new UserInfo();
                user.Id = reader.GetInt32(++i);
                user.Nickname = reader.GetString(++i);
                user.Firstname = reader.GetString(++i);
                user.Lastname = reader.GetString(++i);
                user.Email = reader.GetString(++i);
                user.Points = reader.GetInt32(++i);
                user.Rank = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    user.Sex = reader.GetString(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    user.Country = reader.GetString(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    user.Shortinfo = reader.GetString(i);
                }

                users.Add(user);
            }
            return users;
        }

        public static Collection<UserInfo> SelectUsersWithNumberOfRates(int numberOfRates)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_NUMBER_OF_MOVIE_RATES);
            command.Parameters.AddWithValue("@numberOfRates", numberOfRates);
            SqlDataReader reader = db.Select(command);
            Collection<UserInfo> users = ReadUsersWithNumberOfRates(reader);

            reader.Close();
            db.Close();
            return users;
        }

        private static Collection<UserInfo> ReadUsersWithNumberOfRates(SqlDataReader reader)
        {
            Collection<UserInfo> users = new Collection<UserInfo>();

            while (reader.Read())
            {
                int i = -1;
                UserInfo user = new UserInfo();
                user.Id = reader.GetInt32(++i);
                user.NumberOfRates = reader.GetInt32(++i);

                users.Add(user);
            }
            return users;
        }

        public static int UserValuation()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_VALUATE_USER);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public static int RecalculatePoints() 
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_RECALCULATE_POINTS);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
    }
}
