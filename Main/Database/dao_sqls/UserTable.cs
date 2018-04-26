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
    public class UserTable
    {
        public static String SQL_SELECT = "SELECT * FROM \"UserInfo\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"UserInfo\" WHERE ID=@id";
        public static String SQL_INSERT = "INSERT INTO \"UserInfo\" VALUES (@ID, @nickname, @firstName, @lastName, @email, " + 
            "@points, @rank, @sex, @country, @shortInfo)";
        public static String SQL_DELETE_ID = "DELETE FROM \"UserInfo\" WHERE idUser=@id";
        public static String SQL_UPDATE = "UPDATE \"UserInfo\" SET ID = @ID, nickname = @nickname, firstName = @firstName, " + 
            " lastName = @lastName, email = @email, points = @points, rank = @rank, sex = @sex, country = @country, " + 
            " @shortInfo WHERE idUser=@id";

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
        /// <param name="id">user id</param>
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
        /// <param name="idUser">user id</param>
        /// <returns></returns>
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

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckUser(int idUser, Database pDb)
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

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("CheckUser");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idUser";
            input.DbType = DbType.Int32;
            input.Value = idUser;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            // 3. create output parameters
            SqlParameter output = new SqlParameter();
            output.ParameterName = "@result";
            output.DbType = DbType.String;
            output.Direction = ParameterDirection.Output;
            command.Parameters.Add(output);

            // 4. execute procedure
            int ret = db.ExecuteNonQuery(command);

            // 5. get values of the output parameters
            string result = command.Parameters["@result"].Value.ToString();

            if (pDb == null)
            {
                db.Close();
            }
            return result;
        }

    }
}
