using Main.ORM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            PreviousButton.Enabled = false;
            index = 0;
            movies = MovieTable.Select();
            SetLabelsForMovie();
        }

        public void SetLabelsForMovie()
        {
            NameLabel.Text = movies[index].Title.ToString();
            YearLabel.Text = movies[index].Year.ToString();
            CountryLabel.Text = movies[index].Country.ToString();
            LanguageLabel.Text = movies[index].Language.ToString();
            TimeLabel.Text = movies[index].Time.ToString();
            DescriptionLabel.Text = movies[index].Description.ToString();
            PremiereLabel.Text = movies[index].Premiere.ToLongDateString();
            AwardCheckBox.Checked = movies[index].Award.GetValueOrDefault();

            setActorsLabel(movies[index].Id);
            setDirectorLabel(movies[index].Id);
            setGenresLabel(movies[index].Id);
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

        private void NextButton_Click(object sender, EventArgs e)
        {
            index++;
            SetLabelsForMovie();
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
            SetLabelsForMovie();
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

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
