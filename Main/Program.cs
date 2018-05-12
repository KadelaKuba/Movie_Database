using System;
using Main.ORM;
using Main.ORM.DAO.Sqls;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //TryAllOperationsOfModel();

            Database db = new Database();
            db.Connect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());

            db.Close();

        }

        public static void TryAllOperationsOfModel()
        {
            Database db = new Database();
            db.Connect();

            Console.WriteLine("                 Delete all data from database                    ");
            Console.WriteLine("------------------------------------------------------------------");
            DeleteAllDataFromDatabase();

            Console.WriteLine("\n\n                           User                               ");
            Console.WriteLine("------------------------------------------------------------------");
            UserInfo userInfo = new UserInfo();
            userInfo.Id = 1;
            userInfo.Nickname = "sob28";
            userInfo.Firstname = "Tonda";
            userInfo.Lastname = "Sobota";
            userInfo.Email = "sobota@seznam.cz";
            userInfo.Points = 42;
            userInfo.Rank = "Pokročilý";
            userInfo.Sex = null;
            userInfo.Country = null;
            userInfo.Shortinfo = null;

            UserTable.Insert(userInfo, db);

            userInfo.Lastname = "Nedela";
            UserTable.Update(userInfo, db);

            UserTable.UserValuation(); // 5.7 Ocenění uživatelů
            UserTable.RecalculatePoints(); // 5.8 Přepočet získaných bodů

            Collection<UserInfo> users = UserTable.Select();

            foreach (UserInfo user in users)
            {
                Console.WriteLine(UserTable.Select(user.Id, db).ToString());
            }

            foreach (UserInfo user in UserTable.SelectUsersWithNumberOfRates(0)) //5.6 Pocet ohodnocených filmu větší než parametr
            {
                Console.WriteLine("User ID: " + user.Id + ", Number of rates: " + user.NumberOfRates);
            }

            Console.WriteLine("\n\n                          Director                            ");
            Console.WriteLine("------------------------------------------------------------------");
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

            Console.WriteLine("\n\n                            Movie                             ");
            Console.WriteLine("------------------------------------------------------------------");
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
                Console.WriteLine("Actor number: " + MovieTable.Select_Actor_Number(mov.Id)); //1.6 Výpis filmů a k nim počet obsazených herců
            }

            Console.WriteLine("\n\n                           Genre                              ");
            Console.WriteLine("------------------------------------------------------------------");
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
            }

            Console.WriteLine("\n\n                           Actor                              ");
            Console.WriteLine("------------------------------------------------------------------");
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
            }

            Console.WriteLine("\n\n                          Rating                              ");
            Console.WriteLine("------------------------------------------------------------------");
            Rating rating = new Rating();
            rating.Movie_id = 1;
            rating.User_id = 1;
            rating.Rate = 8.5m;
            rating.Dateofadd = new DateTime(2018, 05, 08);
            rating.Comment = "Nic moc film";

            RatingTable.Insert(rating, db);

            rating.Dateofadd = new DateTime(2018, 05, 09);
            RatingTable.Update(rating, db);

            RatingTable.AddRating(2, 2, rating.Rate, rating.Comment);   // 6.5 Přidání hodnocení

            Collection<Rating> ratings = RatingTable.Select();

            foreach (Rating rate in ratings)
            {
                Console.WriteLine(RatingTable.SelectMovieID(rate.Movie_id, db).ToString());
            }

            Console.WriteLine("\n\n                        Rating History                        ");
            Console.WriteLine("------------------------------------------------------------------");
            RatingHistory ratingHistory = new RatingHistory();
            ratingHistory.Id = 1;
            ratingHistory.Rate = 9.5m;
            ratingHistory.Dateofchange = new DateTime(2018, 04, 30);
            ratingHistory.Comment = "Uleželo se mi to v hlavě";
            ratingHistory.Rating_movie_id = 1;
            ratingHistory.Rating_userinfo_id = 1;

            RatingHistoryTable.Insert(ratingHistory, db);

            ratingHistory.Rate = 9m;
            RatingHistoryTable.Update(ratingHistory, db);

            Collection<RatingHistory> ratingHistories = RatingHistoryTable.Select();

            foreach (RatingHistory ratingHist in ratingHistories)
            {
                Console.WriteLine(RatingHistoryTable.Select(ratingHist.Id, db).ToString());
            }

            Console.WriteLine("\n\n                         Movie Genre                          ");
            Console.WriteLine("------------------------------------------------------------------");
            MovieGenre movieGenre = new MovieGenre();
            movieGenre.Movie_id = 1;
            movieGenre.Genre_id = 1;
            movieGenre.dateOfAdd = new DateTime(2016, 03, 22);

            MovieGenreTable.Insert(movieGenre, db);

            movieGenre.dateOfAdd = new DateTime(2016, 03, 25);
            MovieGenreTable.Update(movieGenre, db);

            Collection<MovieGenre> movieGenres = MovieGenreTable.Select();

            foreach (MovieGenre movieGen in movieGenres)
            {
                Console.WriteLine(MovieGenreTable.SelectMovieId(movieGen.Movie_id, db).ToString());
            }

            Console.WriteLine("\n\n                        Movie Actor                           ");
            Console.WriteLine("------------------------------------------------------------------");
            MovieActor movieActor = new MovieActor();
            movieActor.Movie_id = 1;
            movieActor.Actor_id = 1;
            movieActor.Role = "Hlavní hrdina";
            movieActor.Fee = 22000;

            MovieActorTable.Insert(movieActor, db);

            movieActor.Fee = 25000;
            MovieActorTable.Update(movieActor, db);

            Collection<MovieActor> movieActors = MovieActorTable.Select();

            foreach (MovieActor movieAct in movieActors)
            {
                Console.WriteLine(MovieActorTable.SelectMovieId(movieAct.Movie_id, db).ToString());
            }

            Console.WriteLine("\n\n");
            db.Close();
        }

        public static void DeleteAllDataFromDatabase()
        {
            Collection<RatingHistory> ratingHistories1 = RatingHistoryTable.Select();
            foreach (RatingHistory ratingHist in ratingHistories1)
            {
                RatingHistoryTable.Delete(ratingHist.Id);
            }

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

            Collection<MovieGenre> movieGenres1 = MovieGenreTable.Select();
            foreach (MovieGenre movieGen in movieGenres1)
            {
                MovieGenreTable.Delete(movieGen.Movie_id, movieGen.Genre_id);
            }

            Collection<Genre> genres1 = GenreTable.Select();
            foreach (Genre gen in genres1)
            {
                GenreTable.Delete(gen.Id);
            }

            Collection<MovieActor> movieActors1 = MovieActorTable.Select();
            foreach (MovieActor movieAct in movieActors1)
            {
                MovieActorTable.Delete(movieAct.Actor_id, movieAct.Movie_id);
            }

            Collection<Actor> actors1 = ActorTable.Select();
            foreach (Actor act in actors1)
            {
                ActorTable.Delete(act.Id);
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
        }
    }
}
