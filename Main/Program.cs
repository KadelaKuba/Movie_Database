using System;
using Main.ORM;
using Main.ORM.DAO.Sqls;
using System.Collections.ObjectModel;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            db.Connect();

            UserInfo userInfo = new UserInfo();
            userInfo.Nickname = "sob28";
            userInfo.Firstname = "Tonda";
            userInfo.Lastname = "Sobota";
            userInfo.Email = "sobota@seznam.cz";
            userInfo.Points = 42;
            userInfo.Rank = "Začátečník";
            userInfo.Sex = null;
            userInfo.Country = null;
            userInfo.Shortinfo = null;

            UserTable.Insert(userInfo, db);

            userInfo.Lastname = "Nedela";
            UserTable.Update(userInfo, db);

            Collection<UserInfo> users = UserTable.Select();

            foreach (UserInfo user in users)
            {
                Console.WriteLine(UserTable.Select(user.Id, db).ToString());
                //UserTable.Delete(user.Id);
            }

            db.Close();
        }
    }
}
