using Main.ORM;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Main.ORM.DAO.Sqls;

namespace Main
{
    public partial class MovieDetail : Form
    {
        public Collection<Movie> movies;
        public int index;

        public MovieDetail()
        {
            InitializeComponent();
        }

        private void MovieDetail_Load(object sender, EventArgs e)
        {
            RatingTable.DeleteByUser(UserInfo.TEST_USER_ID);
            PreviousButton.Enabled = false;
            index = 0;
            movies = MovieTable.Select();
            SetDataForMovie();
        }

        public void SetDataForMovie()
        {
            NameLabel.Text = movies[index].Title.ToString();
            YearLabel.Text = movies[index].Year.ToString();
            CountryLabel.Text = movies[index].Country.ToString();
            LanguageLabel.Text = movies[index].Language.ToString();
            TimeLabel.Text = movies[index].Time.ToString();
            DescriptionLabel.Text = movies[index].Description.ToString();
            PremiereLabel.Text = movies[index].Premiere.ToLongDateString();
            AwardCheckBox.Checked = movies[index].Award.GetValueOrDefault();
            AvgRatingLabel.Text = movies[index].AvgRating.ToString("0.##");

            setActorsLabel(movies[index].Id);
            setDirectorLabel(movies[index].Id);
            setGenresLabel(movies[index].Id);
            setMyRatingLabel(movies[index].Id);

            EnableOrDisableAddButton();
            loadCommentsForMovie(movies[index].Id);
        }

        public void EnableOrDisableAddButton()
        {
            if (movies[index].EnabledRating)
            {
                AddRating.Enabled = true;
            }
            else
            {
                AddRating.Enabled = false;
            }
        }

        public void setActorsLabel(int Movie_ID)
        {
            Collection<Actor> actors = ActorTable.SelectActorsForMovie(Movie_ID);
            String actorsString = "";
           
            foreach (Actor actor in actors)
            {
                actorsString += actor.Firstname.ToString();
                actorsString += " ";
                actorsString += actor.Lastname.ToString();
                if (actor != actors.Last())
                {
                    actorsString += ",";
                }
                actorsString += " ";
            }
            ActorsLabel.Text = actorsString;
        }

        public void setDirectorLabel(int Movie_ID)
        {
            Collection<Director> directors = DirectorTable.SelectDirectorForMovie(Movie_ID);
            String directorsString = "";

            foreach (Director director in directors)
            {
                directorsString += director.Firstname.ToString();
                directorsString += " ";
                directorsString += director.Lastname.ToString();
                if (director != directors.Last())
                {
                    directorsString += ",";
                }
                directorsString += " ";
            }
            DirectorLabel.Text = directorsString;
        }

        public void setGenresLabel(int Movie_ID)
        {
            Collection<Genre> genres = GenreTable.SelectGenresForMovie(Movie_ID);
            String GenresString = "";

            foreach (Genre genre in genres)
            {
                GenresString += genre.Name.ToString();
                GenresString += " ";
                if (genre != genres.Last())
                {
                    GenresString += ",";
                }
                GenresString += " ";
            }
            GenresLabel.Text = GenresString;
        }

        public void setMyRatingLabel(int Movie_ID)
        {
            if (RatingTable.SelectByUserIdAndMovieId(UserInfo.TEST_USER_ID, Movie_ID) == null)
            {
                MyRating.Text = "Dosud nehodnoceno";
            }
            else
            {
                MyRating.Text = RatingTable.SelectByUserIdAndMovieId(UserInfo.TEST_USER_ID, Movie_ID).Rate.ToString("0.##");
            }
            
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            index++;
            SetDataForMovie();
            PreviousButton.Enabled = true;

            if (movies.Count() - 1 == index)
            {
                NextButton.Enabled = false;
            }
            else
            {
                NextButton.Enabled = true;
            }
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            index--;
            SetDataForMovie();
            NextButton.Enabled = true;

            if (index == 0)
            {
                PreviousButton.Enabled = false;
            }
            else
            {
                PreviousButton.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RatingTable.AddRating(movies[index].Id, UserInfo.TEST_USER_ID, Convert.ToDecimal(RatingButton.Text), CommentTextBox.Text);
            setMyRatingLabel(movies[index].Id);
            movies[index].EnabledRating = false;
            EnableOrDisableAddButton();
            loadCommentsForMovie(movies[index].Id);
            AvgRatingLabel.Text = MovieTable.SelectAvgRatingForMovie(movies[index].Id).ToString("0.##");
        }

        private void loadCommentsForMovie(int Movie_ID)
        {
            string comment;
            CommentsView.Items.Clear();
            Collection<Rating> ratings = RatingTable.SelectMovieID(Movie_ID);
            foreach (Rating rating in ratings)
            {
                if(rating.Comment == null)
                {
                    comment = "";
                }
                else
                {
                    comment = rating.Comment.ToString();
                }
                UserInfo user = new UserInfo();
                user = UserTable.Select(rating.User_id);
                string[] row = { user.Nickname, rating.Rate.ToString(), comment };
                var listViewItem = new ListViewItem(row);
                CommentsView.Items.Add(listViewItem);
            }
        }
    }
}
