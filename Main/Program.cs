﻿using System;
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

            //------------------------------------------------------------------
            //                  Delete all data from db
            //------------------------------------------------------------------
            Collection<Rating> ratings1 = RatingTable.Select();
            foreach (Rating rate in ratings1)
            {
                RatingTable.Delete(rate.Movie_id, rate.User_id);
            }

            Collection<UserInfo> users1 = UserTable.Select();
            foreach (UserInfo user in users1)
            {
                UserTable.Delete(user.Id);
            }

            Collection<Movie> movies1 = MovieTable.Select();
            foreach (Movie mov in movies1)
            {
                MovieTable.Delete(mov.Id);
            }

            Collection<Director> dirs = DirectorTable.Select();
            foreach (Director dir in dirs)
            {
                DirectorTable.Delete(dir.Id);
            }            

            //------------------------------------------------------------------
            //                          User
            //------------------------------------------------------------------
            Console.WriteLine("\nUser");
            UserInfo userInfo = new UserInfo();
            userInfo.Id = 1;
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
            }

            //------------------------------------------------------------------
            //                          Director
            //------------------------------------------------------------------
            Director director = new Director();
            director.Id = 1;
            director.Firstname = "Jakub";
            director.Lastname = "Novák";
            director.Nationality = "CZ";
            director.Birthplace = "Brno";
            director.Height = 1.5m;            

            DirectorTable.Insert(director, db);

            director.Lastname = "Holý";
            DirectorTable.Update(director, db);

            Collection<Director> directors = DirectorTable.Select();

            foreach (Director dir in directors)
            {
                Console.WriteLine(DirectorTable.Select(dir.Id, db).ToString());
            }

            //------------------------------------------------------------------
            //                          Movie
            //------------------------------------------------------------------
            Movie movie = new Movie();
            movie.Id = 1;
            movie.Title = "Zmizení";
            movie.Year = 2016;
            movie.Time = 215;
            movie.Language = "sobota@seznam.cz";
            movie.Description = "Velice dobrý film";
            movie.Country = "USA";
            movie.Award = true;
            movie.Premiere = new DateTime(2017, 05, 06);
            movie.Director_id = director.Id;

            MovieTable.Insert(movie, db);

            movie.Year = 2018;
            MovieTable.Update(movie, db);

            Collection<Movie> movies = MovieTable.Select();

            foreach (Movie mov in movies)
            {
                Console.WriteLine(MovieTable.Select(mov.Id, db).ToString());
                //MovieTable.Delete(mov.Id);
            }

            //------------------------------------------------------------------
            //                          Genre
            //------------------------------------------------------------------
            Genre genre = new Genre();
            genre.Id = 1;
            genre.Name = "Thriller";
            genre.Description = "Mrazivý žánr";

            GenreTable.Insert(genre, db);

            genre.Name = "Horror";
            GenreTable.Update(genre, db);

            Collection<Genre> genres = GenreTable.Select();

            foreach (Genre gen in genres)
            {
                Console.WriteLine(GenreTable.Select(gen.Id, db).ToString());
                GenreTable.Delete(gen.Id);
            }

            //------------------------------------------------------------------
            //                          Actor
            //------------------------------------------------------------------
            Actor actor = new Actor();
            actor.Id = 1;
            actor.Firstname = "Čeněk";
            actor.Lastname = "Volant";
            actor.Nationality = "CZ";
            actor.Birthplace = "Ostrava";
            actor.Height = 1.5m;

            ActorTable.Insert(actor, db);

            actor.Firstname = "Jozef";
            ActorTable.Update(actor, db);

            Collection<Actor> actors = ActorTable.Select();

            foreach (Actor act in actors)
            {
                Console.WriteLine(ActorTable.Select(act.Id, db).ToString());
                ActorTable.Delete(act.Id);
            }

            //------------------------------------------------------------------
            //                          Rating
            //------------------------------------------------------------------
            Rating rating = new Rating();
            rating.Movie_id = 1;
            rating.User_id = 1;
            rating.Rate = 8.5m;
            rating.Dateofadd = new DateTime(2018, 05, 08);
            rating.Comment = "Nic moc film";

            RatingTable.Insert(rating, db);

            actor.Firstname = "Jozef";
            RatingTable.Update(rating, db);

            Collection<Rating> ratings = RatingTable.Select();

            foreach (Rating rate in ratings)
            {
                Console.WriteLine(RatingTable.SelectMovieID(rate.Movie_id, db).ToString());
                RatingTable.Delete(rate.Movie_id, rate.User_id);
            }



            db.Close();
        }
    }
}
