using System;
using Main.ORM;
using Main.ORM.DAO.Sqls;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            db.Connect();

            UserInfo u = new UserInfo();
            u.Id = 6;
            u.Nickname = "son28";
            u.Firstname = "Tonda";
            u.Lastname = "Sobota";
            u.Email = "Fialová 8, Ostrava, 70833";
            u.Points = 42;
            u.Rank = "Začátčník";
            u.Sex = null;
            u.Country = null;
            u.Shortinfo = null;

            UserTable.Insert(u, db);

            int count1 = UserTable.Select(db).Count;

            Console.WriteLine("#C: " + count1);

            db.Close();
        }
    }
}
